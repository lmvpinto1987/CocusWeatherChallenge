using CocusWeatherChallenge.Constants;
using CocusWeatherChallenge.Core.Models;
using CocusWeatherChallenge.Core.ViewModels;

namespace CocusWeatherChallenge.Views
{
    [QueryProperty(nameof(Weather), NavigationParameters.Weather)]
    public partial class WeatherPage : ContentPage
    {
        public WeatherResultModel Weather
        {
            set
            {
                if (BindingContext is WeatherViewModel vm)
                {
                    vm.Weather = value;
                }
            }
        }

        public WeatherPage(WeatherViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
