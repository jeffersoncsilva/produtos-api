using FE.WebApp.Services;
using FE.WebApp.Services.Interfaces;

namespace FE.WebApp.IoC;

public static class ConfiguraIocCarrinhoCompra
{
	public static void AddCarrinhoCompraSingleton(this IServiceCollection services)
	{
		services.AddSingleton<ICarrinhoCompraServico, CarrinhoCompraServico>();
	}
}