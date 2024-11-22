using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Management.Application.Contracts.Persistence;
using Product.Management.Persistence.DatabaseContext;
using Product.Management.Persistence.Repositories;

namespace Product.Management.Persistence;

public static class PersistanceServicesRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProductManagementContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ProductMgmtConnectionString"));
            //options.ConfigureWarnings(warnings => { warnings.Log(RelationalEventId.PendingModelChangesWarning); });
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IProductsRepository, ProductsRepository>();

        return services;
    }
}
