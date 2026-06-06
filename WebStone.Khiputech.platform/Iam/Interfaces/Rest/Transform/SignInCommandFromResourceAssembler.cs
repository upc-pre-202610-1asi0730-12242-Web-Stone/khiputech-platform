using WebStone.Khiputech.Platform.Iam.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Iam.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Iam.Interfaces.Rest.Transform;


public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource), "SignInResource cannot be null.");

        return new SignInCommand(resource.Username, resource.Password);
    }
}