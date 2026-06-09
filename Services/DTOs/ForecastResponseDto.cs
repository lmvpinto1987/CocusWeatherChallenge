using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CocusWeatherChallenge.Services.DTOs
{
    public sealed class ForecastResponseDto
    {
        [JsonPropertyName("current")]
        public CurrentWeatherDto? Current { get; set; }

        [JsonPropertyName("daily")]
        public DailyWeatherDto? Daily { get; set; }
    }
}
