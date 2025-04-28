using RO.DevTest.FronEnd.Application;

namespace RO.DevTest.WebApp.IoC;

public static class ConfigureMediatR
{
	public static void AddMediatRService(this IServiceCollection services)
	{
		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssemblies(
				typeof(AppFrontEndLayer).Assembly,
				typeof(Program).Assembly
			);
		});
	}
}