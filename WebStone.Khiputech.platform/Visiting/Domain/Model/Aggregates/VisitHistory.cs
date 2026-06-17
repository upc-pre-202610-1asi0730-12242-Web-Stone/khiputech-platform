namespace WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;

public class VisitHistory
{
    public int Id { get; set; }

    public int ArtworkId { get; set; }

    public string SessionId { get; set; } = string.Empty;

    public DateTime VisitedAt { get; set; } = DateTime.UtcNow;
}