namespace WebStone.Khiputech.Platform.Capacity.Interfaces.Rest.Resources;

public record RoomResource(
    int Id,
    string Name,
    int Capacity,
    int CurrentOccupancy,
    double OccupancyPercentage,
    string Status,
    string Description
);