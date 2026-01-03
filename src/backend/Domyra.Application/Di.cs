using System.Reflection;
using Domyra.Application.Contracts;
using Domyra.Application.Features.Validation;
using MediatR;

namespace Domyra.Application;

using Microsoft.Extensions.DependencyInjection;

public static class Di
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
        );
                
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(
                Assembly.GetExecutingAssembly());
        });
        
        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.AssignableTo(typeof(IRequestValidator<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );
        
        return services;
    }
}