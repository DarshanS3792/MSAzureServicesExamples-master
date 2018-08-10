using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using MSAzureServicesExamples.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MSAzureServicesExamples.Services.CosmosDB
{
    public class DocumentDBService : IDocumentDBService
    {
        public List<Employee> Items { get; private set; }
        DocumentClient client;
        readonly Uri collectionLink;

        public DocumentDBService()
        {
            //ConnectionPolicy connectionPolicy = new ConnectionPolicy();

            //Setting read region selection preference
            //connectionPolicy.PreferredLocations.Add(LocationNames.EastUS); //First preference
            //connectionPolicy.PreferredLocations.Add(LocationNames.EastAsia); //Second preference

            client = new DocumentClient(new Uri(AzureConnection.EndpointUri), AzureConnection.PrimaryKey);
            collectionLink = UriFactory.CreateDocumentCollectionUri(AzureConnection.DatabaseId, AzureConnection.CollectionId);
        }

        //public async Task Initialize()
        //{
        //    await client.OpenAsync().ConfigureAwait(false);
        //}

        public async Task CreateDatabaseAsync(string databaseName)
        {
            try
            {
                await client.CreateDatabaseIfNotExistsAsync(new Database
                {
                    Id = databaseName
                });
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }
        }

        public async Task CreateDocumentCollectionAsync(string databaseName, string collectionName)
        {
            try
            {
                await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(AzureConnection.DatabaseId), new DocumentCollection
                {
                    Id = collectionName
                },
                new RequestOptions
                {
                    OfferThroughput = 400
                });
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }
        }

        async Task DeleteDocumentCollection()
        {
            try
            {
                await client.DeleteDocumentCollectionAsync(collectionLink);
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }
        }

        async Task DeleteDatabase()
        {
            try
            {
                await client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(AzureConnection.DatabaseId));
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            Items = new List<Employee>();
            try
            {
                var query = client.CreateDocumentQuery<Employee>(collectionLink).AsDocumentQuery();
                while (query.HasMoreResults)
                {
                    Items.AddRange(await query.ExecuteNextAsync<Employee>());
                }
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }
            return Items;
        }

        public async Task SaveEmployeeDetailsAsync(Employee employee, bool isNewItem)
        {
            try
            {
                if (isNewItem)
                {
                    await client.CreateDocumentAsync(collectionLink, employee);
                }
                else
                {
                    await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(AzureConnection.DatabaseId, AzureConnection.CollectionId, employee.Id), employee);
                }
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            try
            {
                await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(AzureConnection.DatabaseId, AzureConnection.CollectionId, id));
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }
        }

    }
}
