using CocusWeatherChallenge.Core.Models;

namespace CocusWeatherChallenge.Core.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResultModel> GetWeatherAsync(LocationModel location, CancellationToken cancellationToken = default);
    }
}
