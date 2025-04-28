using System.Text.Json.Serialization;

namespace FE.Application.Features.Login.CriarContaCommand;

public class NovaContaResponse
{
	[JsonPropertyName("id")]
	public string Id { get; set; } = string.Empty;

	[JsonPropertyName("user_name")]
	public string UserName { get; set; } = string.Empty;

	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	[JsonPropertyName("email")]
	public string Email { get; set; } = string.Empty;

	[JsonPropertyName("success")]
	public bool Success { get; set; }
}