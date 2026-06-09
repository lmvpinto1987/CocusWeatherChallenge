using CocusWeatherChallenge.Core.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CocusWeatherChallenge.Core.ViewModels
{
    public partial class SearchViewModel : BaseViewModel
    {
        private readonly IGeocodingService _geocodingService;
        private readonly IWeatherService _weatherService;
        private readonly IWeatherCacheService _cacheService;
        private readonly IConnectivityService _connectivityService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private string cityName = string.Empty;

        public SearchViewModel(
            IGeocodingService geocodingService,
            IWeatherService weatherService,
            IWeatherCacheService cacheService,
            IConnectivityService connectivityService,
            INavigationService navigationService)
        {
            _geocodingService = geocodingService;
            _weatherService = weatherService;
            _cacheService = cacheService;
            _connectivityService = connectivityService;
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            if (string.IsNullOrWhiteSpace(CityName))
            {
                SetError(Models.ErrorType.EmptyCity, "Please enter a city name");
                return;
            }

            try
            {
                IsBusy = true;
                ErrorMessage = null;

                if (!_connectivityService.HasInternetAccess)
                {
                    var cachedWeather = await _cacheService.GetAsync(CityName);

                    if (cachedWeather is null)
                    {
                        SetError(Models.ErrorType.OfflineNoCache, "No internet connection and no cached result available.");
                        return;
                    }

                    await _navigationService.GoToWeatherAsync(cachedWeather);
                    return;
                }

                var location = await _geocodingService.GetLocationAsync(CityName);

                if (location is null)
                {
                    SetError(Models.ErrorType.CityNotFound, "City not found.");
                    return;
                }

                var weather = await _weatherService.GetWeatherAsync(location);
                await _cacheService.SaveAsync(CityName, weather);

                await _navigationService.GoToWeatherAsync(weather);
            }
            catch
            {
                var cachedWeather = await _cacheService.GetAsync(CityName);

                if (cachedWeather is not null)
                {
                    await _navigationService.GoToWeatherAsync(cachedWeather);
                    return;
                }
                SetError(Models.ErrorType.Generic, "An error occurred while retrieving weather information. Please try again.");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
