using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Iam.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Iam.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace WebStone.Khiputech.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;


public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    
    public async Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(user => user.Username == username, cancellationToken);
    }

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await Context.Set<User>()
            .AnyAsync(user => user.Username == username, cancellationToken);
    }
}