using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebStone.Khiputech.Platform.Iam.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
/// Indicates that the action or controller requires authentication.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    /// <inheritdoc />
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Skip authorization if action is decorated with [AllowAnonymous]
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata
            .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
        if (allowAnonymous)
            return;

        var user = (User?)context.HttpContext.Items["User"];
        if (user == null)
            context.Result = new UnauthorizedResult();
    }
}