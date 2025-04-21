using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace API.Middlewares;

public static class ExceptionHandlingMiddleware
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(config =>
        {
            config.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                context.Response.ContentType = "application/json";

                if (exception is ValidationException validationException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    var errors = validationException.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    });

                    await context.Response.WriteAsJsonAsync(new
                    {
                        message = "Validation failed.",
                        errors
                    });

                    return;
                }

                // Diğer hatalar
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Internal server error.",
                    detail = exception?.Message
                });
            });
        });
    }
}