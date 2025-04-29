using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BE.Application.Contracts.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BE.Application.Features.Auth.Commands.LoginCommand;

public class LoginCommandHandler(IIdentityAbstractor identityAbstractor, IConfiguration config) : IRequestHandler<LoginCommand, LoginResponse?>
{
    public async Task<LoginResponse?> Handle(LoginCommand request, CancellationToken ct)
    {
        try
        {
            var user = await identityAbstractor.FindUserByEmailAsync(request.Username);
            if (user is null)
                return null;

            var result = await identityAbstractor.PasswordSignInAsync(user, request.Password);

            if (result.Succeeded)
            {
                var expiry = DateTime.Now.AddDays(Convert.ToInt32(config["Jwt:JwtExpiryInDays"]));
                var roles = await identityAbstractor.GetUserRolesAsync(user);
                var claims = new List<Claim>
                {
	                new(ClaimTypes.Name, user.Name),
	                new(ClaimTypes.Email, request.Username)
                };

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                return new LoginResponse()
                {
                    Success = true,
                    AccessToken = GerarTokenAutenticacao(expiry, claims),
                    ExpirationDate = expiry,
                    IssuedAt = DateTime.Now,
                    Roles = roles
                };
            }

            return new LoginResponse();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private string GerarTokenAutenticacao(DateTime expiry, List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:JwtSecurityKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: config["Jwt:JwtIssuer"],
            audience: config["Jwt:JwtAudience"],
            claims: claims,
            expires: expiry,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}