using Newtonsoft.Json;

namespace MSAzureServicesExamples.Models
{
    public class Employee
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "age")]
        public string Age { get; set; }
    }
}
