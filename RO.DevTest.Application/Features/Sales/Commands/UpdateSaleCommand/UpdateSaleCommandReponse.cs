using System.Text.Json.Serialization;

namespace RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand;

public class UpdateSaleCommandReponse
{
    [JsonPropertyName("id")]
    public Guid SaleIdUpdated { get; set; }
}