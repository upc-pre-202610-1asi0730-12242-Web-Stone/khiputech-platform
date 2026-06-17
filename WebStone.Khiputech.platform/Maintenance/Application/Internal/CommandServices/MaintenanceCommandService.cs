using WebStone.Khiputech.Platform.Maintenance.Application.CommandServices;
using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Maintenance.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Domain.Repositories;

namespace WebStone.Khiputech.Platform.Maintenance.Application.Internal.CommandServices;

public class MaintenanceCommandService(
    IMaintenanceTaskRepository taskRepository,
    IUnitOfWork unitOfWork) : IMaintenanceCommandService
{
    public async Task Handle(ScheduleMaintenanceCommand command, CancellationToken ct)
    {
        var task = new MaintenanceTask(command.ArtworkId, command.ArtworkName, command.Reason, command.ScheduledBy);
        await taskRepository.AddAsync(task, ct);
        await unitOfWork.CompleteAsync(ct);
        // Aquí se podría emitir un evento de dominio: "MaintenanceNoticeDeployed"
        // y notificar al sistema externo (Link Management System) para bloquear el QR.
    }

    public async Task Handle(RestoreArtworkAvailabilityCommand command, CancellationToken ct)
    {
        var task = await taskRepository.FindByIdAsync(command.MaintenanceTaskId, ct);
        if (task == null) throw new Exception("Task not found");
        task.Complete();
        taskRepository.Update(task);
        await unitOfWork.CompleteAsync(ct);
        // Emitir evento: "WorkPreparedForExhibition"
    }
}