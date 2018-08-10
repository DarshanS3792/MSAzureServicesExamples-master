using MSAzureServicesExamples.Models;
using MSAzureServicesExamples.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSAzureServicesExamples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeesPage : ContentPage
    {
        public EmployeesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await App.EmployeeDetailsManager.CreateDatabase(AzureConnection.DatabaseId);
            await App.EmployeeDetailsManager.CreateDocumentCollection(AzureConnection.DatabaseId, AzureConnection.CollectionId);
            ((EmployeesPageViewModel)BindingContext).GetEmployeesAsync();
        }

        async void OnItemselected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = e.SelectedItem as Employee;

            var parameter = new Prism.Navigation.NavigationParameters();
            parameter.Add("employeedetails", selectedItem);

            await ((EmployeesPageViewModel)BindingContext).OnNavigatedFromAsync(parameter);
        }
    }
}