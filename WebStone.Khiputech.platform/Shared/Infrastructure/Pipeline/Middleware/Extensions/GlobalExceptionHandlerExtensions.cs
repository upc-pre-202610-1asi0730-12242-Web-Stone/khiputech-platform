using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace WebStone.Khiputech.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;

public static class GlobalExceptionHandlerExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                var response = new { error = exception?.Message ?? "Internal server error" };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            });
        });
    }
}