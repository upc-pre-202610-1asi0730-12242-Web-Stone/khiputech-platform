using Microsoft.EntityFrameworkCore;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Operation.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using WebStone.Khiputech.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace WebStone.Khiputech.Platform.Operation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class AlertConfigurationRepository(AppDbContext context) : BaseRepository<AlertConfiguration>(context), IAlertConfigurationRepository
{
    public async Task<AlertConfiguration?> GetAsync(CancellationToken ct)
        => await Context.Set<AlertConfiguration>().FirstOrDefaultAsync(ct) ?? CreateDefaultIfNotExists();

    private AlertConfiguration? CreateDefaultIfNotExists()
    {
        var existing = Context.Set<AlertConfiguration>().Local.FirstOrDefault();
        if (existing != null) return existing;

        var defaultConfig = new AlertConfiguration(80, 95, "911 - 123 456");
        Context.Set<AlertConfiguration>().Add(defaultConfig);
        Context.SaveChanges(); // se guarda inmediatamente; mejor usar unit of work, pero es una inicialización
        return defaultConfig;
    }
}