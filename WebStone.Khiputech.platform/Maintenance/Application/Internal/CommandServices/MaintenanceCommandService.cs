using WebStone.Khiputech.Platform.Maintenance.Application.CommandServices;
using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Maintenance.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Maintenance.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Domain.Repositories;

namespace WebStone.Khiputech.Platform.Maintenance.Application.Internal.CommandServices;

public class MaintenanceCommandService(
    IMaintenanceTaskRepository taskRepository,
    IUnitOfWork unitOfWork) : IMaintenanceCommandService
{
    public async Task<MaintenanceTask> Handle(ScheduleMaintenanceCommand command, CancellationToken ct)
    {
        // Validar fechas
        if (command.StartDate >= command.EndDate)
            throw new ArgumentException("Start date must be before end date.");
        if (command.StartDate < DateTime.UtcNow)
            throw new ArgumentException("Start date cannot be in the past.");

        var task = new MaintenanceTask(
            command.ArtworkId,
            command.ArtworkName,
            command.StartDate,
            command.EndDate,
            command.Reason
        );

        await taskRepository.AddAsync(task, ct);
        await unitOfWork.CompleteAsync(ct);

        // Aquí se emitiría un evento de dominio: "QR/NFC code blocked for maintenance"
        // Console.WriteLine($"[DOMAIN EVENT] Artwork {command.ArtworkId} blocked for maintenance until {command.EndDate}");

        return task;
    }

    public async Task Handle(RestoreArtworkAvailabilityCommand command, CancellationToken ct)
    {
        var activeTasks = await taskRepository.ListAsync(true, ct);
        var task = activeTasks.FirstOrDefault(t => t.ArtworkId == command.ArtworkId);
    
        if (task == null)
            throw new Exception($"No hay tarea de mantenimiento activa para la obra {command.ArtworkId}");
    
        task.Complete();
        taskRepository.Update(task);
        await unitOfWork.CompleteAsync(ct);
    
    }

    public async Task Handle(CancelMaintenanceCommand command, CancellationToken ct)
    {
        var task = await taskRepository.FindByIdAsync(command.TaskId, ct);
        if (task == null)
            throw new Exception("Maintenance task not found");

        task.Cancel();
        taskRepository.Update(task);
        await unitOfWork.CompleteAsync(ct);
    }
}