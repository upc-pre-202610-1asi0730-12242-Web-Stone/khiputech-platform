namespace WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;

public class Alert
{
    public Alert() { }

    public Alert(string roomName, string type, string message, string? triggeredBy = null)
    {
        RoomName = roomName;
        Type = type; // "moderada", "critica"
        Message = message;
        TriggeredBy = triggeredBy ?? "system";
        CreatedAt = DateTime.UtcNow;
        Status = "active";
    }

    public int Id { get; private set; }
    public string RoomName { get; private set; } = string.Empty;
    public string Type { get; private set; } = string.Empty; // "moderada", "critica"
    public string Message { get; private set; } = string.Empty;
    public string Status { get; private set; } = "active"; // active, resolved
    public string TriggeredBy { get; private set; } = "system";
    public DateTime CreatedAt { get; private set; }
    public DateTime? ResolvedAt { get; private set; }

    public void Resolve()
    {
        Status = "resolved";
        ResolvedAt = DateTime.UtcNow;
    }
}