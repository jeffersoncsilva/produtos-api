using System.Text.Json.Serialization;

namespace BE.Application.Features.Sales.Commands.UpdateSaleCommand;

public class UpdateSaleCommandReponse
{
    [JsonPropertyName("id")]
    public Guid SaleIdUpdated { get; set; }
}