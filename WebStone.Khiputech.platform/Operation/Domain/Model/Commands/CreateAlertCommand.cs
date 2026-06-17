namespace WebStone.Khiputech.Platform.Operation.Domain.Model.Commands;

public record CreateAlertCommand(string RoomName, string Type, string Message, string? TriggeredBy = null);