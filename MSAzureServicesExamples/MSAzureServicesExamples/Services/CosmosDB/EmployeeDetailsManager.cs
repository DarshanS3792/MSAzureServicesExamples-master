using MSAzureServicesExamples.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSAzureServicesExamples.Services.CosmosDB
{
    public class EmployeeDetailsManager
    {
        IDocumentDBService documentDBService;
        public EmployeeDetailsManager(IDocumentDBService service)
        {
            documentDBService = service;
        }
        public Task CreateDatabase(string databaseName)
        {
            return documentDBService.CreateDatabaseAsync(databaseName);
        }
        public Task CreateDocumentCollection(string databaseName, string collectionName)
        {
            return documentDBService.CreateDocumentCollectionAsync(databaseName, collectionName);
        }
        public Task<List<Employee>> GetEmployeesAsync()
        {
            return documentDBService.GetEmployeesAsync();
        }
        public Task SaveEmployeeDetailsAsync(Employee employee, bool isNewItem = false)
        {
            return documentDBService.SaveEmployeeDetailsAsync(employee, isNewItem);
        }
        public Task DeleteEmployeeAsync(Employee employee)
        {
            return documentDBService.DeleteEmployeeAsync(employee.Id);
        }
    }
}
