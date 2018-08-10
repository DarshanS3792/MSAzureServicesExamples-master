namespace MSAzureServicesExamples
{
    public class AzureConnection
    {
        // Azure Cosmos DB
        public static readonly string EndpointUri = "https://xamempcosmos.documents.azure.com:443/";
        public static readonly string PrimaryKey = "yBFtIRtKbYKHUMp53eKtXLeoX4WDttpBDI6v19kRnbq7th82mMybEBdIjGsx1NTfyiamjJEpFAMoA4qnMkefWg==";
        public static readonly string DatabaseId = "EmployeeDB";
        public static readonly string CollectionId = "Employee";

        // Azure Function
        public static readonly string GetEmployeesFunctionUrl = "https://getemployees.azurewebsites.net/api/HttpTriggerCSharp1";
    }
}
