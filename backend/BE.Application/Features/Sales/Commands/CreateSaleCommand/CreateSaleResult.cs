using System.Text.Json.Serialization;

namespace BE.Application.Features.Sales.Commands.CreateSaleCommand;

public class CreateSaleResult
{
    [JsonPropertyName("id")]
    public Guid SaleId { get; set; }
}