using System.Text.Json.Serialization;

namespace OpenAPI.Models
{
    public class ContractDetailsDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("workTotal")]
        public int WorkTotal { get; set; }

        [JsonPropertyName("workDone")]
        public int WorkDone { get; set; }

        [JsonPropertyName("workState")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WorkState WorkState { get; set; }

        [JsonPropertyName("primaryContactFirstname")]
        public string PrimaryContactFirstname { get; set; }

        [JsonPropertyName("primaryContactLastname")]
        public string PrimaryContactLastname { get; set; }

        [JsonPropertyName("primaryContactEmail")]
        public string PrimaryContactEmail { get; set; }
    }
}
