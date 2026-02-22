using Microsoft.Extensions.Caching.Memory;
using MinecraftSkins.Domain.Enums;
using MinecraftSkins.Domain.Exceptions;
using MinecraftSkins.Services.Interfaces.IHttpClients;
using MinecraftSkins.Services.Interfaces.IServices;
using System.Net;
using System.Text.Json; 

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
        public async Task<(decimal Rate, RateSource Source)> GetRateAsync(CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(CacheKey, out decimal cachedRate))
                return (cachedRate, RateSource.Cache);

            try
            {
                var rate = await FetchAndParseRateAsync(cancellationToken);

                _cache.Set(CacheKey, rate, CacheTtl);
                await SaveRateIfChangedAsync(rate, cancellationToken);  
                return (rate, RateSource.ExternalApi);
            }
            catch (Exception)
            {
                return await GetFallbackRateAsync(cancellationToken);
            }
        }

        private async Task<decimal> FetchAndParseRateAsync(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(BtcUsdEndpoint, cancellationToken);

            EnsureSuccessStatusCode(response);

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            using var document = JsonDocument.Parse(json);

            if (!document.RootElement.TryGetProperty("bitcoin", out var btc) ||
                !btc.TryGetProperty("usd", out var usd) ||
                !usd.TryGetDecimal(out var rate) || rate <= 0)
            {
                throw new ExternalServiceException("Invalid or missing BTC/USD rate in response.");
            }

            return rate;
        }

        private void EnsureSuccessStatusCode(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.TooManyRequests)
                throw new ExternalServiceException("External API rate limit exceeded (429).");

            if (!response.IsSuccessStatusCode)
                throw new ExternalServiceException($"External API error: {(int)response.StatusCode}.");
        }
        private async Task<(decimal Rate, RateSource Source)> GetFallbackRateAsync(CancellationToken cancellationToken)
        {
            var fallback = await _btcRateService.GetLastRateAsync(cancellationToken);

            if (fallback != null && DateTime.UtcNow - fallback.FetchedAtUtc <= FallbackMaxAge)
            {
                _cache.Set(CacheKey, fallback.Rate, CacheTtl);
                return (fallback.Rate, RateSource.Fallback);
            }

            throw new ExternalServiceException("External API unavailable and no valid fallback rate found in DB.");
        }

        private async Task SaveRateIfChangedAsync(decimal rate, CancellationToken cancellationToken)
        {
            var lastRate = await _btcRateService.GetLastRateAsync(cancellationToken);

            if (lastRate == null || lastRate.Rate != rate || DateTime.UtcNow - lastRate.FetchedAtUtc >= CacheTtl)
            {
                await _btcRateService.AddRateAsync(rate, cancellationToken);
            }
        }
    }
}
