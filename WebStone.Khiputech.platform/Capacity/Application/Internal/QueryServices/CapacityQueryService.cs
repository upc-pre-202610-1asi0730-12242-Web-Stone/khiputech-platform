using WebStone.Khiputech.Platform.Capacity.Application.QueryServices;
using WebStone.Khiputech.Platform.Capacity.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Capacity.Domain.Repositories;
using WebStone.Khiputech.Platform.Capacity.Interfaces.Rest.Resources;
using WebStone.Khiputech.Platform.Capacity.Interfaces.Rest.Transform;

namespace WebStone.Khiputech.Platform.Capacity.Application.Internal.QueryServices;

public class CapacityQueryService(
    IRoomRepository roomRepository,
    ISensorRepository sensorRepository) : ICapacityQueryService
{
    public async Task<IEnumerable<RoomResource>> Handle(GetRoomsQuery query, CancellationToken ct)
    {
        var rooms = await roomRepository.ListAsync(ct);
        return rooms.Select(RoomResourceFromEntityAssembler.ToResourceFromEntity);
    }

    public async Task<IEnumerable<SensorResource>> Handle(GetSensorsQuery query, CancellationToken ct)
    {
        var sensors = await sensorRepository.ListAsync(ct);
        return sensors.Select(SensorResourceFromEntityAssembler.ToResourceFromEntity);
    }
}