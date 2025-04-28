using System.Text.Json.Serialization;
using MediatR;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

public class LoginCommand : IRequest<LoginResponse?> {
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
