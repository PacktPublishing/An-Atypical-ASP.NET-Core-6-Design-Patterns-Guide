using System.Text.Json.Serialization;

namespace My.Api.Contracts
{
    public class ClientSummaryDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("totalNumberOfContracts")]
        public int TotalNumberOfContracts { get; set; }
        [JsonPropertyName("numberOfOpenContracts")]
        public int NumberOfOpenContracts { get; set; }
    }
}
