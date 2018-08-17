using MSAzureServicesExamples.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace MSAzureServicesExamples.ViewModels
{
    public class EmployeeDetailsPageViewModel : BindableBase, INavigatedAware
    {
        INavigationService _navigationService;
        IPageDialogService _pageDialogService;

        private Employee empData = null;
        private bool isNewEmp;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _age;
        public string Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }

        public ICommand SaveDetailsCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public EmployeeDetailsPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            SaveDetailsCommand = new DelegateCommand(SaveDetails);
            GoBackCommand = new DelegateCommand(GoBack);
        }

        async void SaveDetails()
        {
            Employee employeeDetails = null;

            employeeDetails = new Employee
            {
                Id = empData.Id,
                Age = Age,
                Name = Name
            };

            // Logic App - Send an email whenever a new employee(record) is added to Cosmos DB
            //var json = JsonConvert.SerializeObject(employeeDetails);

            //using (var client = new HttpClient())
            //{
            //    var content = new StringContent(json, Encoding.UTF8, "application/json");
            //    var response = await client.PostAsync(AzureConnection.RequestUrl, content);
            //}

            await App.EmployeeDetailsManager.SaveEmployeeDetailsAsync(employeeDetails, isNewEmp);

            await _pageDialogService.DisplayAlertAsync("Info", "Saved Successfully", "Ok");

            await _navigationService.GoBackAsync();
        }

        async void GoBack()
        {
            await _navigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            object employeeDetails = null;
            parameters.TryGetValue("empdetails", out employeeDetails);
            if (employeeDetails != null)
            {
                empData = (Employee)employeeDetails;
            }

            bool isNewEmployee;
            parameters.TryGetValue("isnewemployee", out isNewEmployee);
            isNewEmp = isNewEmployee;
        }
    }
}
