using CocusWeatherChallenge.Core.Models;
using CocusWeatherChallenge.Core.Services.Interfaces;
using CocusWeatherChallenge.Services.DTOs;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace CocusWeatherChallenge.Services.Implementations
{
    public sealed class OpenMeteoWeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OpenMeteoWeatherService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<WeatherResultModel> GetWeatherAsync(LocationModel location, CancellationToken cancellationToken = default)
        {
            var url =
                "https://api.open-meteo.com/v1/forecast" +
                $"?latitude={location.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}" +
                $"&longitude={location.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}" +
                "&current=temperature_2m,weather_code" +
                "&daily=weather_code,temperature_2m_max,temperature_2m_min" +
                "&forecast_days=5" +
                "&timezone=auto";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<ForecastResponseDto>(url, cancellationToken);

            if (response?.Current is null || response.Daily is null)
                throw new InvalidOperationException("Invalid weather response.");

            var forecast = response.Daily.Time
                .Select((date, index) => new ForecastDayModel
                {
                    Date = DateTime.Parse(date, CultureInfo.InvariantCulture),
                    MaxTemperature = response.Daily.TemperatureMax.ElementAtOrDefault(index),
                    MinTemperature = response.Daily.TemperatureMin.ElementAtOrDefault(index),
                    WeatherDescription = WeatherCodeHelper.GetDescription(
                        response.Daily.WeatherCode.ElementAtOrDefault(index))
                })
                .ToList();

            return new WeatherResultModel
            {
                CityName = location.DisplayName,
                CurrentTemperature = response.Current.Temperature,
                CurrentDescription = WeatherCodeHelper.GetDescription(response.Current.WeatherCode),
                Forecast = forecast
            };
        }

       
    }
}
