namespace WebStone.Khiputech.Platform.Analytics.Domain.Model.Aggregates;


public class ArtworkStat
{
    public ArtworkStat() { }

    public ArtworkStat(int id, string name, string room, string artist, int visits, double retention,
        double engagement, double avgTimeSpent, string trend)
    {
        Id = id;
        Name = name;
        Room = room;
        Artist = artist;
        Visits = visits;
        Retention = retention;
        Engagement = engagement;
        AvgTimeSpent = avgTimeSpent;
        Trend = trend;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Room { get; private set; } = string.Empty;
    public string Artist { get; private set; } = string.Empty;
    public int Visits { get; private set; }
    public double Retention { get; private set; }      // percentage
    public double Engagement { get; private set; }     // score (0-100)
    public double AvgTimeSpent { get; private set; }   // minutes
    public string Trend { get; private set; } = "stable"; // up, down, stable
}