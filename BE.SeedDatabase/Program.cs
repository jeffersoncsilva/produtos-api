// See https://aka.ms/new-console-template for more information

using BE.Application.Contracts.Infrastructure;
using BE.Infrastructure.IoC;
using BE.Persistence;
using BE.Persistence.IoC;
using BE.SeedDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Populando base de dados de produtos para teste.");
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
var services = new ServiceCollection();
services.InjectInfrastructureDependencies(config);
services.InjectPersistenceDependencies();
var serviceProvider = services.BuildServiceProvider();
var db = serviceProvider.GetService<DefaultContext>();
var identity = serviceProvider.GetService<IIdentityAbstractor>();
if(identity is null || db is null)
{
    Console.WriteLine("Erro ao criar os serviços necessários.");
    return;
}
var user = new SeedUsers(identity);
var products = new SeedProducts(db);
var sales = new SeedSales();
Console.WriteLine("Inicando a criação dos dados. Será criado um total de 1.000.000 produtos e 9.000.000 vendas.");
await user.Seed();
await products.Seed(500_000);
await sales.Seed(500_000);

Console.WriteLine("Inserção de dados finlaizada.");