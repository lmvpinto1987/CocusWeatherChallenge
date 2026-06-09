using CocusWeatherChallenge.Core.Models;
using CocusWeatherChallenge.Core.Services.Interfaces;
using System.Net.Http.Json;
using CocusWeatherChallenge.Services.DTOs;

namespace CocusWeatherChallenge.Services.Implementations
{
    public sealed class OpenMeteoGeocodingService : IGeocodingService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OpenMeteoGeocodingService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<LocationModel?> GetLocationAsync(string cityName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                return null;

            var encodedCity = Uri.EscapeDataString(cityName.Trim());
            var url = $"https://geocoding-api.open-meteo.com/v1/search?name={encodedCity}&count=1&language=en&format=json";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<GeocodingResponseDto>(url, cancellationToken);

            var result = response?.Results?.FirstOrDefault();

            if (result is null)
                return null;

            return new LocationModel
            {
                Name = result.Name ?? cityName,
                Country = result.Country ?? string.Empty,
                Latitude = result.Latitude,
                Longitude = result.Longitude
            };
        }

    }
}
