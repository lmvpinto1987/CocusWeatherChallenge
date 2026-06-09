
using CocusWeatherChallenge.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CocusWeatherChallenge.Core.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string? errorMessage;

        [ObservableProperty]
        private ErrorType errorType;

        public bool HasError => ErrorType != ErrorType.None;

        protected void ClearError()
        {
            ErrorType = ErrorType.None;
            ErrorMessage = null;
        }

        protected void SetError(ErrorType type, string message)
        {
            ErrorType = type;
            ErrorMessage = message;
        }

        partial void OnErrorTypeChanged(ErrorType value)
        {
            OnPropertyChanged(nameof(HasError));
        }
    }
}
