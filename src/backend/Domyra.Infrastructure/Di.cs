using Domyra.Application.Contracts.Repositories;
using Domyra.Infrastructure.Persistence;
using Domyra.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
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

    public static IApplicationBuilder AddInfrastructure(this IApplicationBuilder app, bool isDevelopment)
    {
        if (!isDevelopment)
            return app; 
        
        using var scope = app.ApplicationServices.CreateScope(); 
        var db = scope.ServiceProvider.GetRequiredService<DomyraContext>();
        db.Database.Migrate();
        
        return app;
    }
}