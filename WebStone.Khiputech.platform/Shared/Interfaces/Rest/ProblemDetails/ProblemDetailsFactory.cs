using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebStone.Khiputech.Platform.Shared.Application.Model;

namespace WebStone.Khiputech.Platform.Shared.Interfaces.Rest.ProblemDetails;

public class ProblemDetailsFactory
{
    public Microsoft.AspNetCore.Mvc.ProblemDetails CreateProblemDetails(
        ControllerBase controller,
        int statusCode,
        Error? error = null,
        string? detail = null)
    {
        var problem = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Status = statusCode,
            Title = error?.Code ?? "Error",
            Detail = detail ?? error?.Message ?? "An error occurred.",
            Instance = controller.HttpContext.Request.Path
        };
        return problem;
    }
}