using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinecraftSkins.Infrastructure.Data;
using MinecraftSkins.Infrastructure.HttpClients;
using MinecraftSkins.Infrastructure.Repositories;
using MinecraftSkins.Middlewares;
using MinecraftSkins.Services.Interfaces.IServices;
using MinecraftSkins.Services.Logics;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMemoryCache();

builder.Services.AddHttpClient<IRateProvider, RateProvider>(client =>
{
    client.BaseAddress = new Uri("https://api.coingecko.com/api/v3/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "MinecraftSkinsApp/1.0");
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("UserHeader", new OpenApiSecurityScheme
    {
        Description = "Введите ваш User ID (GUID). Заголовок: X-User-Id",
        In = ParameterLocation.Header,
        Name = "X-User-Id",  
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "UserHeader" 
                }
            },
            new List<string>()  
        }
    });
});

builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<ISkinRepository, SkinRepository>();

builder.Services.AddScoped<IRateHistoryService, RateHistoryService>();
builder.Services.AddScoped<IRateDiagnosticsService, RateDiagnosticsService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IPriceCalculator, PriceCalculator>();
builder.Services.AddScoped<ISkinService,  SkinService>();
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseMiddleware<BuyerIdMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
