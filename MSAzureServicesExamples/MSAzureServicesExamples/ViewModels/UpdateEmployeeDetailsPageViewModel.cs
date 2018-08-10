using MSAzureServicesExamples.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;

namespace MSAzureServicesExamples.ViewModels
{
    public class UpdateEmployeeDetailsPageViewModel : BindableBase, INavigatedAware
    {
        INavigationService _navigationService;
        IPageDialogService _pageDialogService;

        private Employee empData = null;

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

        public ICommand UpdateEmployeeDetailsCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }

        public UpdateEmployeeDetailsPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            UpdateEmployeeDetailsCommand = new DelegateCommand(UpdateEmployeeDetails);
            DeleteEmployeeCommand = new DelegateCommand(DeleteEmployee);
            GoBackCommand = new DelegateCommand(GoBack);
        }

        async void UpdateEmployeeDetails()
        {
            Employee employeeDetails = null;

            employeeDetails = new Employee
            {
                Id = empData.Id,
                Age = Age,
                Name = Name
            };

            await App.EmployeeDetailsManager.SaveEmployeeDetailsAsync(employeeDetails);

            await _pageDialogService.DisplayAlertAsync("Info", "Updated Successfully", "Ok");

            await _navigationService.NavigateAsync("EmployeesPage");
        }

        async void DeleteEmployee()
        {
            bool accepted = await _pageDialogService.DisplayAlertAsync("Confirm", "Are you Sure ?", "Yes", "No");
            if (accepted)
            {
                var employee = empData;
                await App.EmployeeDetailsManager.DeleteEmployeeAsync(employee);
                await _pageDialogService.DisplayAlertAsync("Info", "Deleted Successfully", "Ok");
                await _navigationService.NavigateAsync("EmployeesPage");
            }
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
            parameters.TryGetValue("employeedetails", out employeeDetails);
            if (employeeDetails != null)
            {
                empData = (Employee)employeeDetails;
                Name = empData.Name;
                Age = empData.Age;
            }
        }
    }
}
