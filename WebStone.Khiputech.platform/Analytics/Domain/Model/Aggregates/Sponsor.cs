namespace WebStone.Khiputech.Platform.Analytics.Domain.Model.Aggregates;

public class Sponsor
{
    public Sponsor() { }

    public Sponsor(int id, string name, string room, int impressions, decimal contributed, string status)
    {
        Id = id;
        Name = name;
        Room = room;
        Impressions = impressions;
        Contributed = contributed;
        Status = status;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Room { get; private set; } = string.Empty;
    public int Impressions { get; private set; }
    public decimal Contributed { get; private set; }
    public string Status { get; private set; } = "active";
}