using CocusWeatherChallenge.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CocusWeatherChallenge.Core.ViewModels
{
    public partial class WeatherViewModel : BaseViewModel
    {
        [ObservableProperty]
        private WeatherResultModel? weather;
    }
}
