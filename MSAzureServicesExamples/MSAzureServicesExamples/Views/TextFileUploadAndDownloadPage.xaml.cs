using MSAzureServicesExamples.Services.AzureStorage;
using System.IO;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSAzureServicesExamples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextFileUploadAndDownloadPage : ContentPage
    {
        string uploadedFilename;

        public TextFileUploadAndDownloadPage()
        {
            InitializeComponent();

            downloadButton.Clicked += async (sender, e) =>
            {
                if (!string.IsNullOrWhiteSpace(uploadedFilename))
                {
                    activityIndicator.IsRunning = true;

                    var byteData = await AzureStorageService.GetFileAsync(ContainerType.Text, uploadedFilename);
                    var text = Encoding.UTF8.GetString(byteData);
                    downloadEditor.Text = text;

                    activityIndicator.IsRunning = false;
                }
            };

            uploadButton.Clicked += async (sender, e) =>
            {
                if (!string.IsNullOrWhiteSpace(uploadEditor.Text))
                {
                    activityIndicator.IsRunning = true;

                    var byteData = Encoding.UTF8.GetBytes(uploadEditor.Text);
                    uploadedFilename = await AzureStorageService.UploadFileAsync(ContainerType.Text, new MemoryStream(byteData));

                    downloadButton.IsEnabled = true;
                    activityIndicator.IsRunning = false;
                }
            };
        }
    }
}