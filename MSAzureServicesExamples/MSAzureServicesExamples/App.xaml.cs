using MSAzureServicesExamples.Services.CosmosDB;
using MSAzureServicesExamples.ViewModels;
using MSAzureServicesExamples.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MSAzureServicesExamples
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        public static EmployeeDetailsManager EmployeeDetailsManager { get; private set; }

        protected override void OnInitialized()
        {
            InitializeComponent();

            EmployeeDetailsManager = new EmployeeDetailsManager(new DocumentDBService());

            NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>(); // Registering NavigationPage since we need to navigate across pages in MasterDetailPage
            containerRegistry.RegisterForNavigation<CustomMasterDetailPage, CustomMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<EmployeesPage, EmployeesPageViewModel>();
            containerRegistry.RegisterForNavigation<EmployeeDetailsPage, EmployeeDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<UpdateEmployeeDetailsPage, UpdateEmployeeDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<RedisCacheExamplePage, RedisCacheExamplePageViewModel>();
        }
    }
}
