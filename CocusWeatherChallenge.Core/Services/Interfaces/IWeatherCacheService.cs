using CocusWeatherChallenge.Core.Models;

namespace CocusWeatherChallenge.Core.Services.Interfaces
{
    public interface IWeatherCacheService
    {
        Task SaveAsync(string cityName, WeatherResultModel weather);
        Task<WeatherResultModel?> GetAsync(string cityName);
    }
}
