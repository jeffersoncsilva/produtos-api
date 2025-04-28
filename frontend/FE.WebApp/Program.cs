using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FE.WebApp;
using FE.WebApp.IoC;
using FE.WebApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<ConfirmationService>();
builder.Services.AddMediatRService();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddTokenServices();
builder.Services.ConfigureHttpFactory(builder.Configuration);

await builder.Build().RunAsync();
