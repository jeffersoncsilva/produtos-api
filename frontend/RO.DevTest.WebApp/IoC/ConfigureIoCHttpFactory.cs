using RO.DevTest.FronEnd.Application;

namespace RO.DevTest.WebApp.IoC;

public static class ConfigureIoCHttpFactory
{
	public static void ConfigureHttpFactory(this IServiceCollection services, IConfiguration config)
	{
		var uriBase = Environment.GetEnvironmentVariable("BACKEND_API");

		if (string.IsNullOrEmpty(uriBase))
			uriBase = config["Api:BaseUrl"];

		if(string.IsNullOrWhiteSpace(uriBase))
			uriBase = "http://localhost:5087";

		services.AddHttpClient(HttpConfiguration.HttpClientName, op => op.BaseAddress = new Uri(uriBase));
	}
}