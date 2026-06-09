namespace CocusWeatherChallenge.Core.Models
{
    public sealed class WeatherResultModel
    {
        public string CityName { get; set; } = string.Empty;
        public double CurrentTemperature { get; set; }
        public string CurrentDescription { get; set; } = string.Empty;
        public List<ForecastDayModel> Forecast { get; set; } = [];
        public bool IsFromCache { get; set; }
    }
}
