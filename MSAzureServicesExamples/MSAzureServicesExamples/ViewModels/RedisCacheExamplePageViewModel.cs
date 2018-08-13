using MSAzureServicesExamples.Models;
using MSAzureServicesExamples.Services.RedisCache;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.Generic;

namespace MSAzureServicesExamples.ViewModels
{
    public class RedisCacheExamplePageViewModel : BindableBase
    {
        INavigationService _navigationService;
        private readonly RedisCacheService _rediscacheService;

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

        private string _timeTaken;
        public string TimeTaken
        {
            get { return _timeTaken; }
            set { SetProperty(ref _timeTaken, value); }
        }

        public RedisCacheExamplePageViewModel(INavigationService navigationService, RedisCacheService rediscacheService)
        {
            _navigationService = navigationService;
            _rediscacheService = rediscacheService;

            GetDataFromRedisCache();

            IsBusy = true;
        }

        private async void GetDataFromRedisCache()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var employees = await _rediscacheService.GetEmployees();

            watch.Stop();

            var elapsedMs = watch.ElapsedMilliseconds;

            Employees = employees;

            IsBusy = false;

            TimeTaken = "Time taken to fetch the data is " + elapsedMs + " ms";
        }
    }
}
