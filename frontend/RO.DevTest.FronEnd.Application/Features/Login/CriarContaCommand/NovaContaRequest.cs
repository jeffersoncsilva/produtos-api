using System.Text.Json.Serialization;
using MediatR;

namespace RO.DevTest.FronEnd.Application.Features.Login.CriarContaCommand;

public class NovaContaRequest : IRequest<NovaContaResponse?>
{
	[JsonPropertyName("user_name")]
	public string UserName { get; set; } = string.Empty;
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;
	[JsonPropertyName("email")]
	public string Email { get; set; } = string.Empty;
	[JsonPropertyName("password")]
	public string Password { get; set; } = string.Empty;
	[JsonPropertyName("password_confirmation")]
	public string PasswordConfirmation { get; set; } = string.Empty;
	[JsonPropertyName("user_role")]
	public UserRoles Role { get; set; }

}