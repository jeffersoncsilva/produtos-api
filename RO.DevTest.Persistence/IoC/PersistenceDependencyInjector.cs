using BE.Application.Contracts.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RO.DevTest.Persistence.Repositories;

namespace RO.DevTest.Persistence.IoC;

public static class PersistenceDependencyInjector {
    /// <summary>
    /// Inject the dependencies of the Persistence layer into an
    /// <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to inject the dependencies into
    /// </param>
    /// <returns>
    /// The <see cref="IServiceCollection"/> with dependencies injected
    /// </returns>
    public static IServiceCollection InjectPersistenceDependencies(this IServiceCollection services) {
        var connectionString = Environment.GetEnvironmentVariable("RO_DEV_TEST_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
            connectionString = "Server=localhost;port=5432;Database=rodevtest;User Id=postgres;Password=root;";
        services.AddDbContext<DefaultContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Scoped);
        services.AddScoped<IProductsRepository, ProductRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        return services;
    }
}
