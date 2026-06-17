namespace WebStone.Khiputech.Platform.Iam.Interfaces.Rest.Resources;


public record AuthenticatedUserResource(
    int Id,
    string Username,
    string Type,
    string[] Permissions,
    string Token);