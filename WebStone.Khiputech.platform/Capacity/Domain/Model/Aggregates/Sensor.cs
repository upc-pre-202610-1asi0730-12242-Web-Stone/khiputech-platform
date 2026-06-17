namespace WebStone.Khiputech.Platform.Capacity.Domain.Model.Aggregates;

public class Sensor
{
    public Sensor() { }

    public Sensor(string name, string location, string type, string status = "active")
    {
        Name = name;
        Location = location;
        Type = type; // entry, motion, temperature
        Status = status;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Location { get; private set; } = string.Empty;
    public string Type { get; private set; } = string.Empty;
    public string Status { get; private set; } = "active"; // active, inactive, maintenance
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Sensor UpdateStatus(string status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }
}