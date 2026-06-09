namespace CocusWeatherChallenge.Core.Services.Interfaces
{
    public interface IConnectivityService
    {
        bool HasInternetAccess { get; }
    }
}
