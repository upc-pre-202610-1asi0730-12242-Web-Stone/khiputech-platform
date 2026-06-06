using WebStone.Khiputech.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;

namespace WebStone.Khiputech.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;

public static class RequestAuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestAuthorization(this IApplicationBuilder builder)
        => builder.UseMiddleware<RequestAuthorizationMiddleware>();
}