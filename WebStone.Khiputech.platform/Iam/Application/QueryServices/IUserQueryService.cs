using WebStone.Khiputech.Platform.Iam.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Iam.Domain.Model.Queries;

namespace WebStone.Khiputech.Platform.Iam.Application.QueryServices;


public interface IUserQueryService
{
    Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken);


    Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken);


    Task<User?> Handle(GetUserByUsernameQuery query, CancellationToken cancellationToken);
}