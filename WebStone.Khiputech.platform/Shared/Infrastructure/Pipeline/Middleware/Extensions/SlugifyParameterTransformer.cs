using Microsoft.AspNetCore.Routing;

namespace WebStone.Khiputech.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;

/// <summary>
/// Converts route parameter names to kebab-case (e.g., "AuthenticationController" -> "authentication").
/// </summary>
public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value == null) return null;
        // Convertir a kebab-case: "AuthenticationController" -> "authentication"
        return value.ToString()!
            .Replace("Controller", "")
            .ToLowerInvariant();
    }
}