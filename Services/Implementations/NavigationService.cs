
using CocusWeatherChallenge.Constants;
using CocusWeatherChallenge.Core.Services.Interfaces;

namespace CocusWeatherChallenge.Services.Implementations
{
    public sealed class NavigationService : INavigationService
    {
        public Task GoToWeatherAsync(object weather)
        {
            return Shell.Current.GoToAsync(Routes.Weather, new Dictionary<string, object>
            {
                [NavigationParameters.Weather] = weather
            });
        }
    }
}
