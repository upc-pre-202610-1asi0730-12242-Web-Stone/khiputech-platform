namespace WebStone.Khiputech.Platform.Capacity.Interfaces.Rest.Resources;

public record SensorResource(
    int Id,
    string Name,
    string Location,
    string Type,
    string Status
);