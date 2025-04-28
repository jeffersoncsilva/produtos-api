using System.Text.Json.Serialization;

namespace FE.Application.Features.Login.RealizarLoginCommand;

public class LoginResponse
{
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("access_token")]
	public string? AccessToken { get; set; } = null;


	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("refresh_token")]
	public string? RefreshToken { get; set; } = null;

	[JsonPropertyName("issued_at")]
	public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

	[JsonPropertyName("expiration_date")]
	public DateTime ExpirationDate { get; set; }
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

	[JsonPropertyName("roles")]
	public IList<string>? Roles { get; set; } = null;

	[JsonPropertyName("success")]
	public bool Success { get; set; } = false;
}