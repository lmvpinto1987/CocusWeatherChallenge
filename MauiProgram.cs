
using CocusWeatherChallenge.Core.Services.Interfaces;
using CocusWeatherChallenge.Core.ViewModels;
using CocusWeatherChallenge.Services.Implementations;
using CocusWeatherChallenge.Views;
using Microsoft.Extensions.Logging;

namespace CocusWeatherChallenge;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddHttpClient();

        builder.Services.AddSingleton<IGeocodingService, OpenMeteoGeocodingService>();
        builder.Services.AddSingleton<IWeatherService, OpenMeteoWeatherService>();
        builder.Services.AddSingleton<IWeatherCacheService, WeatherCacheService>();

        builder.Services.AddTransient<SearchViewModel>();
        builder.Services.AddTransient<WeatherViewModel>();

        builder.Services.AddTransient<SearchPage>();
        builder.Services.AddTransient<WeatherPage>();

        builder.Services.AddSingleton<IConnectivityService, ConnectivityService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
