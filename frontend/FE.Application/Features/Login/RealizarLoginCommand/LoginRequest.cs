using System.Text.Json.Serialization;
using MediatR;

namespace FE.Application.Features.Login.RealizarLoginCommand;

public class LoginRequest : IRequest<LoginResponse?>
{
	[JsonPropertyName("username")]
	public string? Email { get; set; }
	[JsonPropertyName("password")]
	public string? Senha { get; set; }
}