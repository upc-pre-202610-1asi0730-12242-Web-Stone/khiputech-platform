using WebStone.Khiputech.Platform.Iam.Application.CommandServices;
using WebStone.Khiputech.Platform.Iam.Application.Internal.OutboundServices;
using WebStone.Khiputech.Platform.Iam.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Iam.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Iam.Domain.Model.Errors;
using WebStone.Khiputech.Platform.Iam.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Application.Model;
using WebStone.Khiputech.Platform.Shared.Domain.Repositories;

namespace WebStone.Khiputech.Platform.Iam.Application.Internal.CommandServices;

/// <summary>
/// Implementation of user command services (sign-in and sign-up).
/// </summary>
public class UserCommandService(
    IUserRepository userRepository,
    IHashingService hashingService,
    ITokenService tokenService,
    IUnitOfWork unitOfWork)
    : IUserCommandService
{
    /// <inheritdoc />
    public async Task<Result<(User user, string token)>> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        // Buscar usuario por nombre de usuario
        var user = await userRepository.FindByUsernameAsync(command.Username, cancellationToken);
        
        // Si no existe o la contraseña no es válida, retornar error
        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return Result<(User, string)>.Failure(IamErrors.InvalidCredentials);

        // Generar token JWT (incluirá type y permissions en los claims)
        var token = tokenService.GenerateToken(user);

        return Result<(User, string)>.Success((user, token));
    }

    /// <inheritdoc />
    public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        // Verificar si el nombre de usuario ya existe
        var exists = await userRepository.ExistsByUsernameAsync(command.Username, cancellationToken);
        if (exists)
            return Result.Failure(IamErrors.UsernameAlreadyTaken);

        // Hash de la contraseña
        var hashedPassword = hashingService.HashPassword(command.Password);

        // Crear nuevo usuario con tipo por defecto "public" y permisos vacíos
        var user = new User(command.Username, hashedPassword, "public", "");

        try
        {
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(IamErrors.UserCreationFailed);
        }
    }
}