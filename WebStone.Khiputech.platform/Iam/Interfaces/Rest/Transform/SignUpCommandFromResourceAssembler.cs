using WebStone.Khiputech.Platform.Iam.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Iam.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Iam.Interfaces.Rest.Transform;


public static class SignUpCommandFromResourceAssembler
{

    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource), "SignUpResource cannot be null.");

        return new SignUpCommand(resource.Username, resource.Password);
    }
}