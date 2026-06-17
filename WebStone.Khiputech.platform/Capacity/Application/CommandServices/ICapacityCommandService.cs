using WebStone.Khiputech.Platform.Capacity.Domain.Model.Commands;

namespace WebStone.Khiputech.Platform.Capacity.Application.CommandServices;

public interface ICapacityCommandService
{
    Task Handle(UpdateRoomOccupancyCommand command, CancellationToken ct);
}