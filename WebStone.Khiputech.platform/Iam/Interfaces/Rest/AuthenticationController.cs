using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using WebStone.Khiputech.Platform.Iam.Application.CommandServices;
using WebStone.Khiputech.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using WebStone.Khiputech.Platform.Iam.Interfaces.Rest.Resources;
using WebStone.Khiputech.Platform.Iam.Interfaces.Rest.Transform;
using WebStone.Khiputech.Platform.Iam.Resources;
using WebStone.Khiputech.Platform.Resources.Errors;
using WebStone.Khiputech.Platform.Shared.Interfaces.Rest.ProblemDetails;

namespace WebStone.Khiputech.Platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(
    IUserCommandService userCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IStringLocalizer<IamMessages> iamLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly IStringLocalizer<IamMessages> _iamLocalizer = iamLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;


    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sign in",
        Description = "Authenticates a user and returns a JWT token",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid username or password")]
    public async Task<IActionResult> SignIn(
        [FromBody] SignInResource signInResource,
        CancellationToken cancellationToken)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var result = await userCommandService.Handle(signInCommand, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromSignInResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            userAndToken => Ok(AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(
                userAndToken.user,
                userAndToken.token))
        );
    }


    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sign-up",
        Description = "Creates a new user account",
        OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was created successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The user could not be created")]
    public async Task<IActionResult> SignUp(
        [FromBody] SignUpResource signUpResource,
        CancellationToken cancellationToken)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
        var result = await userCommandService.Handle(signUpCommand, cancellationToken);

        return IamActionResultAssembler.ToActionResultFromSignUpResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            () => Ok(new { message = _iamLocalizer["UserCreatedSuccessfully"] })
        );
    }
}