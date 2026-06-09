using CocusWeatherChallenge.Core.Services.Interfaces;

namespace CocusWeatherChallenge.Services.Implementations
{
    public class ConnectivityService : IConnectivityService
    {
        public bool HasInternetAccess =>
            Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
    }
}
