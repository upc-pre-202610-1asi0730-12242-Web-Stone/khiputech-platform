namespace WebStone.Khiputech.Platform.Capacity.Domain.Model.Commands;

public record UpdateRoomOccupancyCommand(int RoomId, int NewOccupancy);