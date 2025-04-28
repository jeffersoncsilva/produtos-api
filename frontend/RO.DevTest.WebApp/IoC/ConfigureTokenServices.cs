using Microsoft.AspNetCore.Components.Authorization;
using FE.Application.Contracts;
using RO.DevTest.WebApp.Services;

namespace RO.DevTest.WebApp.IoC;

public static class ConfigureTokenServices
{
	public static void AddTokenServices(this IServiceCollection services)
	{
		services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
		services.AddScoped<IAuthenticationTokenService, AuthenticationTokenService>();
	}
}