namespace CocusWeatherChallenge.Core.Services.Interfaces
{
    public interface INavigationService
    {
        Task GoToWeatherAsync(object weather);
    }
}
