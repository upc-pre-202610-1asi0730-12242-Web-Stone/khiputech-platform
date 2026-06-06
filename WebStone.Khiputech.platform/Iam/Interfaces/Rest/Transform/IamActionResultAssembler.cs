using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebStone.Khiputech.Platform.Iam.Domain.Model;
using WebStone.Khiputech.Platform.Iam.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Resources.Errors;
using WebStone.Khiputech.Platform.Shared.Application.Model;
using WebStone.Khiputech.Platform.Shared.Interfaces.Rest.ProblemDetails;

namespace WebStone.Khiputech.Platform.Iam.Interfaces.Rest.Transform;

public static class IamActionResultAssembler
{
    private static int ToStatusCodeFromIamError(IamError error)
    {
        return error switch
        {
            IamError.InvalidCredentials => StatusCodes.Status401Unauthorized,
            IamError.UsernameAlreadyTaken => StatusCodes.Status409Conflict,
            IamError.UserNotFound => StatusCodes.Status404NotFound,
            IamError.OperationCancelled => StatusCodes.Status409Conflict,
            IamError.DatabaseError => StatusCodes.Status500InternalServerError,
            IamError.InternalServerError => StatusCodes.Status500InternalServerError,
            IamError.ExternalServiceError => StatusCodes.Status503ServiceUnavailable,
            _ => StatusCodes.Status400BadRequest
        };
    }

    private static IamError MapToIamError(Error error)
    {
        return error.Code switch
        {
            "Iam.InvalidCredentials" => IamError.InvalidCredentials,
            "Iam.UsernameAlreadyTaken" => IamError.UsernameAlreadyTaken,
            "Iam.UserNotFound" => IamError.UserNotFound,
            "Iam.OperationCancelled" => IamError.OperationCancelled,
            "Iam.DatabaseError" => IamError.DatabaseError,
            "Iam.InternalServerError" => IamError.InternalServerError,
            "Iam.ExternalServiceError" => IamError.ExternalServiceError,
            _ => IamError.None
        };
    }

    public static IActionResult ToActionResultFromSignInResult(
        ControllerBase controller,
        Result<(User user, string token)> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<(User user, string token), IActionResult> successAction)
    {
        if (result.IsSuccess)
            return successAction(result.Value!);

        var iamError = MapToIamError(result.Error!);
        var statusCode = ToStatusCodeFromIamError(iamError);
        var problemDetails = problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Error?.Message);
        
        // Convert ProblemDetails to IActionResult (ObjectResult)
        return new ObjectResult(problemDetails) { StatusCode = statusCode };
    }

    public static IActionResult ToActionResultFromSignUpResult(
        ControllerBase controller,
        Result result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<IActionResult> successAction)
    {
        if (result.IsSuccess)
            return successAction();

        var iamError = MapToIamError(result.Error!);
        var statusCode = ToStatusCodeFromIamError(iamError);
        var problemDetails = problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Error?.Message);
        
        return new ObjectResult(problemDetails) { StatusCode = statusCode };
    }
}