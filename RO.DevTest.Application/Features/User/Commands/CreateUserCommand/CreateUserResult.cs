using System.Text.Json.Serialization;

namespace RO.DevTest.Application.Features.User.Commands.CreateUserCommand;

public record CreateUserResult {
    
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
    
    
    public CreateUserResult () { }

    public CreateUserResult(Domain.Entities.User user) { 
        Id = user.Id;
        UserName = user.UserName!;
        Email = user.Email!;
        Name = user.Name!;
        Success = true;
    }
}
