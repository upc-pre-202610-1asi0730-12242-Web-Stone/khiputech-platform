namespace WebStone.Khiputech.Platform.Maintenance.Domain.Model.Aggregates;

public class MaintenanceTask
{
    public MaintenanceTask() { }

    public MaintenanceTask(int artworkId, string artworkName, DateTime startDate, DateTime endDate, string reason)
    {
        ArtworkId = artworkId;
        ArtworkName = artworkName;
        StartDate = startDate;
        EndDate = endDate;
        Reason = reason;
        Status = "pending";
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public int ArtworkId { get; private set; }
    public string ArtworkName { get; private set; } = string.Empty;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string Reason { get; private set; } = string.Empty;
    public string Status { get; private set; } = "pending";
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public bool IsActive => Status == "pending" || Status == "in_progress";
    public bool IsOverdue => Status != "completed" && Status != "cancelled" && EndDate < DateTime.UtcNow;

    public MaintenanceTask Start()
    {
        if (Status != "pending")
            throw new InvalidOperationException("Only pending tasks can be started.");
        Status = "in_progress";
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public MaintenanceTask Complete()
    {
        if (Status != "in_progress")
            throw new InvalidOperationException("Only in-progress tasks can be completed.");
        Status = "completed";
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public MaintenanceTask Cancel()
    {
        if (Status == "completed")
            throw new InvalidOperationException("Completed tasks cannot be cancelled.");
        Status = "cancelled";
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public void UpdateDates(DateTime start, DateTime end)
    {
        StartDate = start;
        EndDate = end;
        UpdatedAt = DateTime.UtcNow;
    }
}