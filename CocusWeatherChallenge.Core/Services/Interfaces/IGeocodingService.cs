using CocusWeatherChallenge.Core.Models;

namespace CocusWeatherChallenge.Core.Services.Interfaces
{
    public interface IGeocodingService
    {
        Task<LocationModel?> GetLocationAsync(string cityName, CancellationToken cancellationToken = default);
    }
}
