using Prism.Commands;
using Prism.Navigation;
using System.Windows.Input;

namespace MSAzureServicesExamples.ViewModels
{
    public class AzureStorageExamplePageViewModel
    {
        INavigationService _navigationService;

        public ICommand GoToTextFileUploadAndDownloadPageCommand { get; set; }
        public ICommand GoToTextFileBrowserPageCommand { get; set; }

        public AzureStorageExamplePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GoToTextFileUploadAndDownloadPageCommand = new DelegateCommand(GoToTextFileUploadAndDownloadPage);
            GoToTextFileBrowserPageCommand = new DelegateCommand(GoToTextFileBrowserPage);
        }

        private async void GoToTextFileBrowserPage()
        {
            await _navigationService.NavigateAsync("TextFileBrowserPage");
        }

        private async void GoToTextFileUploadAndDownloadPage()
        {
            await _navigationService.NavigateAsync("TextFileUploadAndDownloadPage");
        }
    }
}
