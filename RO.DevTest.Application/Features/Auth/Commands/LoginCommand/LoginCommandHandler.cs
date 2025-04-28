using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RO.DevTest.Application.Contracts.Infrastructure;

namespace RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

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
                return new LoginResponse()
                {
                    Success = true,
                    AccessToken = GerarTokenAutenticacao(expiry),
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

    private string GerarTokenAutenticacao(DateTime expiry)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:JwtSecurityKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            expires: expiry,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}