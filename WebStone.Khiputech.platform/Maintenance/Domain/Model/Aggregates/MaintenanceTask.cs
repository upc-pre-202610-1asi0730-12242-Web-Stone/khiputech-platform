namespace WebStone.Khiputech.Platform.Maintenance.Domain.Model.Aggregates;

public class MaintenanceTask
{
    public MaintenanceTask() { }

    public MaintenanceTask(int artworkId, string artworkName, string reason, string scheduledBy)
    {
        ArtworkId = artworkId;
        ArtworkName = artworkName;
        Reason = reason;
        ScheduledBy = scheduledBy;
        Status = "pending"; // pending, in_progress, completed, cancelled
        ScheduledAt = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public int ArtworkId { get; private set; }
    public string ArtworkName { get; private set; } = string.Empty;
    public string Reason { get; private set; } = string.Empty;
    public string Status { get; private set; } = "pending";
    public string? ScheduledBy { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public void Start()
    {
        if (Status != "pending") throw new InvalidOperationException("Only pending tasks can be started");
        Status = "in_progress";
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (Status != "in_progress") throw new InvalidOperationException("Only in-progress tasks can be completed");
        Status = "completed";
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == "completed") throw new InvalidOperationException("Completed tasks cannot be cancelled");
        Status = "cancelled";
        UpdatedAt = DateTime.UtcNow;
    }
}