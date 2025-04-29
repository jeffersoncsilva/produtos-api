using System.Text.Json.Serialization;

namespace BE.Application.Features.Sales.Commands.CreateSaleCommand;

public class CreateSaleResult
{
    [JsonPropertyName("id")]
    public Guid SaleId { get; init; }
    
    [JsonPropertyName("success")]
    public bool Success { get; init; }

    [JsonPropertyName("errors")] 
    public List<string>? Errors { get; init; }
}