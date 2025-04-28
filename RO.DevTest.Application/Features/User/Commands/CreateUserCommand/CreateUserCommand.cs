using System.Text.Json.Serialization;
using MediatR;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Application.Features.User.Commands.CreateUserCommand;

public class CreateUserCommand : IRequest<CreateUserResult> {
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

    [JsonPropertyName("user_role")] public UserRoles? Role { get; set; }

    public Domain.Entities.User AssignTo() {
        return new Domain.Entities.User {
            UserName = UserName,
            Email = Email,
            Name = Name,
        };
    }
}
