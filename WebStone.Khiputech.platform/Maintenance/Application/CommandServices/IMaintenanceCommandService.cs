using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Commands;

namespace WebStone.Khiputech.Platform.Maintenance.Application.CommandServices;

public interface IMaintenanceCommandService
{
    Task Handle(ScheduleMaintenanceCommand command, CancellationToken ct);
    Task Handle(RestoreArtworkAvailabilityCommand command, CancellationToken ct);
}