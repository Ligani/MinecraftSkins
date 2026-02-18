using Microsoft.Extensions.Caching.Memory;
using MinecraftSkins.Domain.Enums;
using MinecraftSkins.Domain.Exceptions;
using MinecraftSkins.Domain.Models;
using MinecraftSkins.Infrastructure.Repositories;
using MinecraftSkins.Services.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;   

namespace MinecraftSkins.Infrastructure.HttpClients
{
        public class RateProvider : IRateProvider
        {
            public const string BtcUsdEndpoint = "simple/price?ids=bitcoin&vs_currencies=usd&precision=8";
            private const string CacheKey = "btc_usd_rate";

            private static readonly TimeSpan CacheTtl = TimeSpan.FromSeconds(60);
            private static readonly TimeSpan FallbackMaxAge = TimeSpan.FromMinutes(10);

            private readonly HttpClient _httpClient;
            private readonly IMemoryCache _cache;

            private readonly IRateHistoryService _btcRateService;

            public RateProvider(HttpClient httpClient, IMemoryCache cache, IRateHistoryService btcRateService)
            {
                _httpClient = httpClient;
            _cache = cache;
            _btcRateService = btcRateService;
        }

        public async Task<(decimal, RateSource)> GetRateAsync(CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(CacheKey, out decimal cachedRate))
                return (cachedRate, RateSource.Cache);

            decimal rate;

            try
            {
                var response = await _httpClient.GetAsync(BtcUsdEndpoint, cancellationToken);

                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                    throw new ExternalServiceException("External API rate limit exceeded (429).");

                if (!response.IsSuccessStatusCode)
                    throw new ExternalServiceException($"External API returned status code {(int)response.StatusCode}.");

                var json = await response.Content.ReadAsStringAsync(cancellationToken);

                using var document = JsonDocument.Parse(json);

                if (!document.RootElement.TryGetProperty("bitcoin", out var bitcoinProp))
                    throw new ExternalServiceException("JSON does not contain 'bitcoin' property.");

                if (!bitcoinProp.TryGetProperty("usd", out var usdProp))
                    throw new ExternalServiceException("JSON does not contain 'usd' property for bitcoin.");

                if (!usdProp.TryGetDecimal(out rate) || rate <= 0)
                    throw new ExternalServiceException("Invalid BTC/USD rate value in JSON.");

                _cache.Set(CacheKey, rate, CacheTtl);

                var lastRate = await _btcRateService.GetLastRateAsync(cancellationToken);

                if (lastRate == null || lastRate.Rate != rate || DateTime.UtcNow - lastRate.FetchedAtUtc >= CacheTtl)
                {
                    await _btcRateService.AddRateAsync(rate, cancellationToken);
                }
                return (rate, RateSource.ExternalApi);
            }
            catch
            {
                var fallback = await _btcRateService.GetLastRateAsync(cancellationToken);

                if (fallback != null && DateTime.UtcNow - fallback.FetchedAtUtc <= FallbackMaxAge)
                {
                    _cache.Set(CacheKey, fallback.Rate, CacheTtl);
                    return (fallback.Rate, RateSource.Fallback);
                }
                else
                {
                    throw new ExternalServiceException("External API unavailable and no recent rate in DB.");
                }
            }
        }
    }
}
