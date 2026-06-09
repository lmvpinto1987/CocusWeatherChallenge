using CocusWeatherChallenge.Core.Models;
using CocusWeatherChallenge.Core.Services.Interfaces;
using System.Text.Json;

namespace CocusWeatherChallenge.Services.Implementations
{
    public sealed class WeatherCacheService : IWeatherCacheService
    {
        private const string CachePrefix = "weather_cache_";

        public Task SaveAsync(string cityName, WeatherResultModel weather)
        {
            var key = GetKey(cityName);
            var json = JsonSerializer.Serialize(weather);

            Preferences.Default.Set(key, json);

            return Task.CompletedTask;
        }

        public Task<WeatherResultModel?> GetAsync(string cityName)
        {
            var key = GetKey(cityName);
            var json = Preferences.Default.Get<string?>(key, null);

            if (string.IsNullOrWhiteSpace(json))
                return Task.FromResult<WeatherResultModel?>(null);

            var result = JsonSerializer.Deserialize<WeatherResultModel>(json);

            if (result is not null)
                result.IsFromCache = true;

            return Task.FromResult(result);
        }

        private static string GetKey(string cityName)
        {
            return $"{CachePrefix}{cityName.Trim().ToLowerInvariant()}";
        }
    }
}
