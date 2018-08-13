using MSAzureServicesExamples.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSAzureServicesExamples.Services.RedisCache
{
    public class RedisCacheService
    {
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = AzureConnection.ConnectionString;
            return ConnectionMultiplexer.Connect(cacheConnection);
        });

        private IDatabase redisCache;
        private static bool dataAdded = false;

        public RedisCacheService()
        {
            redisCache = lazyConnection.Value.GetDatabase();

            List<Employee> employeeList = new List<Employee>();
            if (redisCache != null && dataAdded == false)
            {
                for (int i = 0; i < 100; i++)
                {
                    employeeList.Add(new Employee
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Darshan S",
                        Age = "26"
                    });
                }

                redisCache.StringSet("employeeList", JsonConvert.SerializeObject(employeeList)); // Example to set list

                // Example to set single object
                //cache.StringSet("employee", JsonConvert.SerializeObject(employee));

                dataAdded = true;
            }
        }

        public async Task<List<Employee>> GetEmployees()
        {
            // Example to retrieve single object from cache
            //Employee employeeFromCache = JsonConvert.DeserializeObject<Employee>(cache.StringGet("employee"));

            // Example to retrieve list from cache
            var employeesFromCache = await Task.Run(() => JsonConvert.DeserializeObject<List<Employee>>(redisCache.StringGet("employeeList")));

            return employeesFromCache;
        }
    }
}
