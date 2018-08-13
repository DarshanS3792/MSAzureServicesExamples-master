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

        // Azure Redis Cache
        public static readonly string ConnectionString = "redisexample.redis.cache.windows.net:6380,password=IoM3I0eLcyQizwb7He+Zo4jbVFRsSEPWV63wcq8KA6Q=,ssl=True,abortConnect=False";

        // Azure Storage
        public static readonly string StorageConnection = "DefaultEndpointsProtocol=https;AccountName=azurestorageex;AccountKey=iuWvGzElW3HqwwmY/cLlzd/QF1VBrqx9ngy803IM5tg68J64B+K1sCu1TCI4pxG7Sqlf24RhgbnMU6Ldx0QEgg==;EndpointSuffix=core.windows.net";
    }
}
