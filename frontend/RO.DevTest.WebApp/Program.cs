using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RO.DevTest.FronEnd.Application;
using RO.DevTest.WebApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMediatR(cfg =>
{
	cfg.RegisterServicesFromAssemblies(
		typeof(AppFrontEndLayer).Assembly,
		typeof(Program).Assembly
	);
});

var uriBase = Environment.GetEnvironmentVariable("RO_DEVTES_BACKEND_API");
if (string.IsNullOrEmpty(uriBase))
	uriBase = "http://localhost:5087";
builder.Services.AddHttpClient(HttpConfiguration.HttpClientName, op => op.BaseAddress = new Uri(uriBase));

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
