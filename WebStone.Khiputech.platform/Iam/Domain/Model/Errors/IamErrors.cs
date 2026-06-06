using WebStone.Khiputech.Platform.Shared.Application.Model;  // ← Cambia esto

namespace WebStone.Khiputech.Platform.Iam.Domain.Model.Errors;

public static class IamErrors
{
    public static readonly Error InvalidCredentials = new("Iam.InvalidCredentials", "Invalid username or password.");
    public static readonly Error UsernameAlreadyTaken = new("Iam.UsernameAlreadyTaken", "The username is already taken.");
    public static readonly Error UserCreationFailed = new("Iam.UserCreationFailed", "An error occurred while creating the user.");
    public static readonly Error UserNotFound = new("Iam.UserNotFound", "User not found.");
}