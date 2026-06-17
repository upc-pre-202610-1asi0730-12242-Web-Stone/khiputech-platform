using WebStone.Khiputech.Platform.Iam.Application.Internal.OutboundServices;
using WebStone.Khiputech.Platform.Iam.Application.QueryServices;
using WebStone.Khiputech.Platform.Iam.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace WebStone.Khiputech.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
        {
            await next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Missing or invalid token.");
            return;
        }

        // Usamos el cancellation token de la petición si es necesario
        var userId = await tokenService.ValidateToken(token);
        if (userId == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid token.");
            return;
        }

        var query = new GetUserByIdQuery(userId.Value);
        var user = await userQueryService.Handle(query, context.RequestAborted);
        context.Items["User"] = user;

        await next(context);
    }
}