using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CocusWeatherChallenge.Services.DTOs
{
    public sealed class GeocodingResponseDto
    {
        [JsonPropertyName("results")]
        public List<GeocodingResultDto>? Results { get; set; }
    }
}
