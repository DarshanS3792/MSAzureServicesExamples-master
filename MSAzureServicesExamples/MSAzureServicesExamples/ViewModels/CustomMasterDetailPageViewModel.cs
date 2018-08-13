using Prism.Commands;
using Prism.Navigation;

namespace MSAzureServicesExamples.ViewModels
{
    public class CustomMasterDetailPageViewModel
    {
        INavigationService _navigationService;
        public DelegateCommand<string> OnNavigateCommand { get; set; }

        public CustomMasterDetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            OnNavigateCommand = new DelegateCommand<string>(NavigateAsync);
        }

        async void NavigateAsync(string page)
        {
            await _navigationService.NavigateAsync(page);
        }
    }
}
