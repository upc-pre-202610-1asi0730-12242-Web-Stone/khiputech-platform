using WebStone.Khiputech.Platform.Iam.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Iam.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Iam.Interfaces.Rest.Transform;


public static class AuthenticatedUserResourceFromEntityAssembler
{

    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user), "User cannot be null.");
        
        if (string.IsNullOrEmpty(token))
            throw new ArgumentException("Token cannot be null or empty.", nameof(token));

        var permissionsArray = string.IsNullOrEmpty(user.Permissions)
            ? Array.Empty<string>()
            : user.Permissions.Split(',', StringSplitOptions.RemoveEmptyEntries);

        return new AuthenticatedUserResource(
            user.Id,
            user.Username,
            user.Type,
            permissionsArray,
            token
        );
    }
}