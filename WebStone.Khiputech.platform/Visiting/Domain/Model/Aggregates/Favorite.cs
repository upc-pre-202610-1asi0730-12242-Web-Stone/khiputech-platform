namespace WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;

public class Favorite
{
    public int Id { get; set; }

    public int ArtworkId { get; set; }

    public string SessionId { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}