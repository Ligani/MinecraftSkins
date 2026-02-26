using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinecraftSkins.Filters;
using MinecraftSkins.Infrastructure.Data;
using MinecraftSkins.Infrastructure.HttpClients;
using MinecraftSkins.Infrastructure.Repositories;
using MinecraftSkins.Middlewares;
using MinecraftSkins.Services.Interfaces.IHttpClients;
using MinecraftSkins.Services.Interfaces.IRepositories;
using MinecraftSkins.Services.Interfaces.IServices;
using MinecraftSkins.Services.Logics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMemoryCache();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddHttpClient<IRateProvider, RateProvider>(client =>
{
    client.BaseAddress = new Uri("https://api.coingecko.com/api/v3/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "MinecraftSkinsApp/1.0");
});

const string ReactAppPolicy = "ReactAppPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: ReactAppPolicy, policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:8081").AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("X-Pagination");
        });
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
app.UseRouting();
app.UseCors(ReactAppPolicy);

app.UseMiddleware<BuyerIdMiddleware>();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        Console.WriteLine("Применяем миграции...");
        context.Database.Migrate();
        Console.WriteLine("Миграции успешно применены!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка при применении миграций: {ex.Message}");
    }
}

app.Run();
