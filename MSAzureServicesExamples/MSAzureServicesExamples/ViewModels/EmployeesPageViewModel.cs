using MSAzureServicesExamples.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MSAzureServicesExamples.ViewModels
{
    public class EmployeesPageViewModel : BindableBase, INavigatedAware
    {
        INavigationService _navigationService;

        private string _username;
        public string UserName
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private List<Employee> _employees;
        public List<Employee> Employees
        {
            get { return _employees; }
            set { SetProperty(ref _employees, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }

        private string _timeTaken;
        public string TimeTaken
        {
            get { return _timeTaken; }
            set { SetProperty(ref _timeTaken, value); }
        }

        public ICommand AddEmployeeCommand { get; set; }

        public EmployeesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;

            AddEmployeeCommand = new DelegateCommand(AddEmployee);

            GetEmployeesAsync();

            IsBusy = true;

            IsVisible = false;
        }

        public async void GetEmployeesAsync()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // Get data by calling CosmosDB API 
            var data = await App.EmployeeDetailsManager.GetEmployeesAsync();

            watch.Stop();

            var elapsedMs = watch.ElapsedMilliseconds;

            Employees = data;

            IsBusy = false;
            IsVisible = true;

            TimeTaken = "Time taken to fetch the data is " + elapsedMs + " ms";

            // Get data by calling Azure Function API
            //using (var client = new HttpClient())
            //{
            //    var url = AzureConnection.GetEmployeesFunctionUrl;

            //    var res = await client.GetAsync(url);

            //    var json = await res.Content.ReadAsStringAsync();

            //    var data = JsonConvert.DeserializeObject<List<Employee>>(json);
            //    Employees = data;

            //    IsBusy = false;
            //    IsVisible = true;
            //}
        }

        async void AddEmployee()
        {
            Employee employeeDetails = null;

            employeeDetails = new Employee
            {
                Id = Guid.NewGuid().ToString()
            };

            NavigationParameters navparams = new NavigationParameters();
            navparams.Add("empdetails", employeeDetails);
            navparams.Add("isnewemployee", true);

            await _navigationService.NavigateAsync("EmployeeDetailsPage", navparams);
        }

        public async Task OnNavigatedFromAsync(NavigationParameters parameters)
        {
            await _navigationService.NavigateAsync("UpdateEmployeeDetailsPage", parameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("UserName"))
            {
                UserName = parameters.GetValue<string>("UserName");
            }
        }
    }
}
