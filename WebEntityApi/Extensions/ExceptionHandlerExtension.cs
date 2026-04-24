using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using WebEntityApi.Exceptions;

namespace WebEntityApi.Extensions;

public static class ExceptionHandlerExtension
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerFeature>();
                var ex = feature?.Error;

                var (statusCode, message) = ex switch
                {
                    NotFoundException e => (HttpStatusCode.NotFound, e.Message),
                    ConflictException e => (HttpStatusCode.Conflict, e.Message),
                    BadRequestException e => (HttpStatusCode.BadRequest, e.Message),
                    _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
                };

                if (statusCode == HttpStatusCode.InternalServerError)
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Unhandled exception");
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;

                var body = JsonSerializer.Serialize(new { error = message });
                await context.Response.WriteAsync(body);
            });
        });
    }
}
