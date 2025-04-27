using System.Text.Json.Serialization;

namespace RO.DevTest.Application.Features.Sales.Commands.DeleteSaleCommand;

public class DeleteSaleCommandResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
}