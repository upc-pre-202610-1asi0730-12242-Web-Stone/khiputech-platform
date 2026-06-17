namespace WebStone.Khiputech.Platform.Capacity.Domain.Model.Aggregates;

public class Room
{
    // Constructor vacío para EF
    public Room() { }

    public Room(string name, int capacity, string? description = null)
    {
        Name = name;
        Capacity = capacity;
        Description = description ?? string.Empty;
        CurrentOccupancy = 0;
        Status = "normal"; // normal, moderate, critical, overcapacity
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Capacity { get; private set; }
    public int CurrentOccupancy { get; private set; }
    public string Status { get; private set; } = "normal";
    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public double OccupancyPercentage => Capacity == 0 ? 0 : (double)CurrentOccupancy / Capacity * 100;

    public Room UpdateOccupancy(int newOccupancy)
    {
        CurrentOccupancy = newOccupancy;
        UpdateStatus();
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public Room IncrementOccupancy(int amount = 1)
    {
        return UpdateOccupancy(CurrentOccupancy + amount);
    }

    public Room DecrementOccupancy(int amount = 1)
    {
        return UpdateOccupancy(Math.Max(0, CurrentOccupancy - amount));
    }

    private void UpdateStatus()
    {
        if (CurrentOccupancy > Capacity)
            Status = "overcapacity";
        else if (CurrentOccupancy >= Capacity * 0.95)
            Status = "critical";
        else if (CurrentOccupancy >= Capacity * 0.80)
            Status = "moderate";
        else
            Status = "normal";
    }
}