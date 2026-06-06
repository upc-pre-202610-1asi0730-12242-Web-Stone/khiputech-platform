using WebStone.Khiputech.Platform.Iam.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Iam.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Shared.Application.Model;

namespace WebStone.Khiputech.Platform.Iam.Application.CommandServices;


public interface IUserCommandService
{

    Task<Result<(User user, string token)>> Handle(SignInCommand command, CancellationToken cancellationToken);


    Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken);
}