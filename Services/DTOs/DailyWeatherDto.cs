using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CocusWeatherChallenge.Services.DTOs
{
    public sealed class DailyWeatherDto
    {
        [JsonPropertyName("time")]
        public List<string> Time { get; set; } = [];

        [JsonPropertyName("weather_code")]
        public List<int> WeatherCode { get; set; } = [];

        [JsonPropertyName("temperature_2m_max")]
        public List<double> TemperatureMax { get; set; } = [];

        [JsonPropertyName("temperature_2m_min")]
        public List<double> TemperatureMin { get; set; } = [];
    }
}
