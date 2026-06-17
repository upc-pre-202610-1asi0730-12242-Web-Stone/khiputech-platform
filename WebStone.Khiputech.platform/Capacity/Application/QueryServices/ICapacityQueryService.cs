using WebStone.Khiputech.Platform.Capacity.Domain.Model.Queries;
using WebStone.Khiputech.Platform.Capacity.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Capacity.Application.QueryServices;

public interface ICapacityQueryService
{
    Task<IEnumerable<RoomResource>> Handle(GetRoomsQuery query, CancellationToken ct);
    Task<IEnumerable<SensorResource>> Handle(GetSensorsQuery query, CancellationToken ct);
}