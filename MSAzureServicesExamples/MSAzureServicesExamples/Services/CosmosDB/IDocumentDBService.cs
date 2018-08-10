using MSAzureServicesExamples.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSAzureServicesExamples.Services.CosmosDB
{
    public interface IDocumentDBService
    {
        Task CreateDatabaseAsync(string databaseName);
        Task CreateDocumentCollectionAsync(string databaseName, string collectionName);
        Task<List<Employee>> GetEmployeesAsync();
        Task SaveEmployeeDetailsAsync(Employee emp, bool isNewEmployee);
        Task DeleteEmployeeAsync(string id);
    }
}
