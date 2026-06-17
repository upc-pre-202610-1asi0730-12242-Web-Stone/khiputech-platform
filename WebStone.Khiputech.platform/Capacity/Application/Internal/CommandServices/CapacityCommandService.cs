using WebStone.Khiputech.Platform.Capacity.Application.CommandServices;
using WebStone.Khiputech.Platform.Capacity.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Capacity.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Domain.Repositories;

namespace WebStone.Khiputech.Platform.Capacity.Application.Internal.CommandServices;

public class CapacityCommandService(
    IRoomRepository roomRepository,
    IUnitOfWork unitOfWork) : ICapacityCommandService
{
    public async Task Handle(UpdateRoomOccupancyCommand command, CancellationToken ct)
    {
        var room = await roomRepository.FindByIdAsync(command.RoomId, ct);
        if (room == null)
            throw new Exception($"Room {command.RoomId} not found.");

        room.UpdateOccupancy(command.NewOccupancy);
        roomRepository.Update(room);
        await unitOfWork.CompleteAsync(ct);
    }
}