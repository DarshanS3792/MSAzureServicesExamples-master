using MSAzureServicesExamples.Services.AzureStorage;
using System;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSAzureServicesExamples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextFileBrowserPage : ContentPage
    {
        string fileName;

        public TextFileBrowserPage()
        {
            InitializeComponent();

            getFilesButton.Clicked += OnGetFileListButtonClicked;

            listView.ItemSelected += async (sender, e) =>
            {
                fileName = e.SelectedItem.ToString();
                var byteData = await AzureStorageService.GetFileAsync(ContainerType.Text, fileName);
                var text = Encoding.UTF8.GetString(byteData);
                editor.Text = text;
                deleteButton.IsEnabled = true;
            };

            deleteButton.Clicked += async (sender, e) =>
            {
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    bool isDeleted = await AzureStorageService.DeleteFileAsync(ContainerType.Text, fileName);
                    if (isDeleted)
                    {
                        OnGetFileListButtonClicked(sender, e);
                    }
                }
            };
        }

        async void OnGetFileListButtonClicked(object sender, EventArgs e)
        {
            activityIndicator.IsRunning = true;

            var fileList = await AzureStorageService.GetFilesListAsync(ContainerType.Text);
            listView.ItemsSource = fileList;

            editor.Text = string.Empty;

            deleteButton.IsEnabled = false;

            activityIndicator.IsRunning = false;
        }
    }
}