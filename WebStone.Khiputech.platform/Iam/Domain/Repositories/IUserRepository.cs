using WebStone.Khiputech.Platform.Iam.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Shared.Domain.Repositories;

namespace WebStone.Khiputech.Platform.Iam.Domain.Repositories;


public interface IUserRepository : IBaseRepository<User>
{

    Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken);

    Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken);
}