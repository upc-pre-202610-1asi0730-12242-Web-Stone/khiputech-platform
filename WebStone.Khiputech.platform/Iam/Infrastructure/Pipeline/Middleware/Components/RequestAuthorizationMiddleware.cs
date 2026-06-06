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
        ITokenService tokenService,
        CancellationToken cancellationToken)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
        {
            await next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("Token missing");

        var userId = await tokenService.ValidateToken(token);
        if (userId == null)
            throw new UnauthorizedAccessException("Invalid token");

        var query = new GetUserByIdQuery(userId.Value);
        var user = await userQueryService.Handle(query, cancellationToken);
        context.Items["User"] = user;

        await next(context);
    }
}