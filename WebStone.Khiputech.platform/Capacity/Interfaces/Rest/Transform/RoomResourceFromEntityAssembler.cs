using WebStone.Khiputech.Platform.Capacity.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Capacity.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Capacity.Interfaces.Rest.Transform;

public static class RoomResourceFromEntityAssembler
{
    public static RoomResource ToResourceFromEntity(Room room)
    {
        return new RoomResource(
            room.Id,
            room.Name,
            room.Capacity,
            room.CurrentOccupancy,
            room.OccupancyPercentage,
            room.Status,
            room.Description
        );
    }
}