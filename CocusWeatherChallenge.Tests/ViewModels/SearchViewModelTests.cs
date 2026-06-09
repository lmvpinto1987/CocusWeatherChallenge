using CocusWeatherChallenge.Core.Models;
using CocusWeatherChallenge.Core.Services.Interfaces;
using CocusWeatherChallenge.Core.ViewModels;
using Moq;

namespace CocusWeatherChallenge.Tests.ViewModels
{
    public class SearchViewModelTests
    {
        // Validates that an empty city name shows the correct validation error.
        [Fact]
        public async Task SearchCommand_WhenCityNameIsEmpty_ShouldSetEmptyCityError()
        {
            var viewModel = CreateViewModel();

            viewModel.CityName = string.Empty;

            await viewModel.SearchCommand.ExecuteAsync(null);

            Assert.Equal(ErrorType.EmptyCity, viewModel.ErrorType);
            Assert.True(viewModel.HasError);
            Assert.False(viewModel.IsBusy);
        }

        // Validates that an invalid/non-existing city returns the CityNotFound error.
        [Fact]
        public async Task SearchCommand_WhenCityDoesNotExist_ShouldSetCityNotFoundError()
        {
            var geocodingService = new Mock<IGeocodingService>();

            geocodingService
                .Setup(x => x.GetLocationAsync("InvalidCity", It.IsAny<CancellationToken>()))
                .ReturnsAsync((LocationModel?)null);

            var viewModel = CreateViewModel(geocodingService: geocodingService);

            viewModel.CityName = "InvalidCity";

            await viewModel.SearchCommand.ExecuteAsync(null);

            Assert.Equal(ErrorType.CityNotFound, viewModel.ErrorType);
            Assert.True(viewModel.HasError);
            Assert.False(viewModel.IsBusy);
        }

        // Validates the successful flow:
        // - get location
        // - get weather
        // - save cache
        // - navigate to weather page

        [Fact]
        public async Task SearchCommand_WhenWeatherIsRetrieved_ShouldSaveCacheAndNavigate()
        {
            var location = new LocationModel
            {
                Name = "Porto",
                Country = "Portugal",
                Latitude = 41.15,
                Longitude = -8.61
            };

            var weather = new WeatherResultModel
            {
                CityName = "Porto, Portugal",
                CurrentTemperature = 22,
                CurrentDescription = "Clear sky"
            };

            var geocodingService = new Mock<IGeocodingService>();
            var weatherService = new Mock<IWeatherService>();
            var cacheService = new Mock<IWeatherCacheService>();
            var navigationService = new Mock<INavigationService>();

            geocodingService
                .Setup(x => x.GetLocationAsync("Porto", It.IsAny<CancellationToken>()))
                .ReturnsAsync(location);

            weatherService
                .Setup(x => x.GetWeatherAsync(location, It.IsAny<CancellationToken>()))
                .ReturnsAsync(weather);

            var viewModel = CreateViewModel(
                geocodingService: geocodingService,
                weatherService: weatherService,
                cacheService: cacheService,
                navigationService: navigationService);

            viewModel.CityName = "Porto";

            await viewModel.SearchCommand.ExecuteAsync(null);

            cacheService.Verify(x => x.SaveAsync("Porto", weather), Times.Once);
            navigationService.Verify(x => x.GoToWeatherAsync(weather), Times.Once);

            Assert.Equal(ErrorType.None, viewModel.ErrorType);
            Assert.False(viewModel.HasError);
            Assert.False(viewModel.IsBusy);
        }

        // Validates offline behavior when cached data exists:
        // the app should use cached weather and navigate successfully.
        [Fact]
        public async Task SearchCommand_WhenOfflineAndCacheExists_ShouldNavigateWithCachedWeather()
        {
            var cachedWeather = new WeatherResultModel
            {
                CityName = "Porto, Portugal",
                CurrentTemperature = 20,
                CurrentDescription = "Cached result",
                IsFromCache = true
            };

            var cacheService = new Mock<IWeatherCacheService>();
            var connectivityService = new Mock<IConnectivityService>();
            var navigationService = new Mock<INavigationService>();

            connectivityService
                .Setup(x => x.HasInternetAccess)
                .Returns(false);

            cacheService
                .Setup(x => x.GetAsync("Porto"))
                .ReturnsAsync(cachedWeather);

            var viewModel = CreateViewModel(
                cacheService: cacheService,
                connectivityService: connectivityService,
                navigationService: navigationService);

            viewModel.CityName = "Porto";

            await viewModel.SearchCommand.ExecuteAsync(null);

            navigationService.Verify(x => x.GoToWeatherAsync(cachedWeather), Times.Once);

            Assert.Equal(ErrorType.None, viewModel.ErrorType);
            Assert.False(viewModel.HasError);
            Assert.False(viewModel.IsBusy);
        }


        // Validates offline behavior when no cached data exists:
        // the app should show an OfflineNoCache error.
        [Fact]
        public async Task SearchCommand_WhenOfflineAndCacheDoesNotExist_ShouldSetOfflineNoCacheError()
        {
            var cacheService = new Mock<IWeatherCacheService>();
            var connectivityService = new Mock<IConnectivityService>();

            connectivityService
                .Setup(x => x.HasInternetAccess)
                .Returns(false);

            cacheService
                .Setup(x => x.GetAsync("Porto"))
                .ReturnsAsync((WeatherResultModel?)null);

            var viewModel = CreateViewModel(
                cacheService: cacheService,
                connectivityService: connectivityService);

            viewModel.CityName = "Porto";

            await viewModel.SearchCommand.ExecuteAsync(null);

            Assert.Equal(ErrorType.OfflineNoCache, viewModel.ErrorType);
            Assert.True(viewModel.HasError);
            Assert.False(viewModel.IsBusy);
        }

        // Helper method used to create the ViewModel with mocked dependencies.
        // Allows overriding only the mocks needed for each test.
        private static SearchViewModel CreateViewModel(
            Mock<IGeocodingService>? geocodingService = null,
            Mock<IWeatherService>? weatherService = null,
            Mock<IWeatherCacheService>? cacheService = null,
            Mock<IConnectivityService>? connectivityService = null,
            Mock<INavigationService>? navigationService = null)
        {
            geocodingService ??= new Mock<IGeocodingService>();
            weatherService ??= new Mock<IWeatherService>();
            cacheService ??= new Mock<IWeatherCacheService>();
            navigationService ??= new Mock<INavigationService>();

            if (connectivityService is null)
            {
                connectivityService = new Mock<IConnectivityService>();
                connectivityService
                    .Setup(x => x.HasInternetAccess)
                    .Returns(true);
            }

            return new SearchViewModel(
                geocodingService.Object,
                weatherService.Object,
                cacheService.Object,
                connectivityService.Object,
                navigationService.Object);
        }
    }
}
