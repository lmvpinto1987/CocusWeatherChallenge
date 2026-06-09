namespace CocusWeatherChallenge.Core.Models
{
    public sealed class ForecastDayModel
    {
        public DateTime Date { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public string WeatherDescription { get; set; } = string.Empty;
    }
}
