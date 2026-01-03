using Domyra.Application.Exceptions;

namespace Domyra.Api;

public static class ErrorHandler
{
    public static IApplicationBuilder UseErrorHandler(
        this IApplicationBuilder builder)
    {
        builder.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;

                if (exception is DomyraNotFoundException)
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsJsonAsync(new { error = exception.Message });
                }

                if (exception is DomyraValidationException)
                {
                    context.Response.StatusCode = 400;
                    var validationException = exception as DomyraValidationException;
                    var validationResult = validationException?.GetValidationResult(); 
                    
                    await context.Response.WriteAsJsonAsync(validationResult);
                }
                else
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred." });
                }
            });
        });
        
        return builder;
    }    
}