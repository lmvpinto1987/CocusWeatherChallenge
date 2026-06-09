using CocusWeatherChallenge.Core.ViewModels;

namespace CocusWeatherChallenge.Views
{
    public partial class SearchPage : ContentPage
    {
        public SearchPage(SearchViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CityEntry?.Unfocus();
        }
    }
}
