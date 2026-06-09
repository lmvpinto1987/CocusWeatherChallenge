using CocusWeatherChallenge.Constants;
using CocusWeatherChallenge.Views;

namespace CocusWeatherChallenge;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(Routes.Weather, typeof(WeatherPage));
    }
}
