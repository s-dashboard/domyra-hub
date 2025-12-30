using Domyra.Application.Contracts.Repositories;
using Domyra.Infrastructure.Persistence;
using Domyra.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domyra.Infrastructure;

public static class Di
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DomyraContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );
        services.AddScoped<IDeviceRepository, DeviceRepository>();
        
        return services;
    }
}