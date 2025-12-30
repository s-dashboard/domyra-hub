using System.Reflection;
using Domyra.Application.Contracts.Repositories;

namespace Domyra.Application;

using Microsoft.Extensions.DependencyInjection;

public static class Di
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(
                Assembly.GetExecutingAssembly());
        });
        
        return services;
    }
}