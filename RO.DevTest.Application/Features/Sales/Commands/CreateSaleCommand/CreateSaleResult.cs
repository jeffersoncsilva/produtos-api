using System.Text.Json.Serialization;

namespace RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;

public class CreateSaleResult
{
    [JsonPropertyName("id")]
    public Guid SaleId { get; set; }
}