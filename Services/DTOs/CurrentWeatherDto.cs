using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CocusWeatherChallenge.Services.DTOs
{
    public sealed class CurrentWeatherDto
    {
        [JsonPropertyName("temperature_2m")]
        public double Temperature { get; set; }

        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }
    }
}
