using WebStone.Khiputech.Platform.Capacity.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Capacity.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Capacity.Interfaces.Rest.Transform;

public static class SensorResourceFromEntityAssembler
{
    public static SensorResource ToResourceFromEntity(Sensor sensor)
    {
        return new SensorResource(
            sensor.Id,
            sensor.Name,
            sensor.Location,
            sensor.Type,
            sensor.Status
        );
    }
}