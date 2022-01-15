using System.Text.Json.Serialization;

namespace WebApi.Models;

public class CustomerSummaryDto
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
