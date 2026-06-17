namespace WebStone.Khiputech.Platform.Analytics.Domain.Model.Aggregates;


public class VisitorStat
{
    public VisitorStat() { }

    public VisitorStat(DateOnly date, int totalVisitors, int scannedArtworks, double avgDuration, int nps)
    {
        Date = date;
        TotalVisitors = totalVisitors;
        ScannedArtworks = scannedArtworks;
        AvgDuration = avgDuration;
        Nps = nps;
    }

    public DateOnly Date { get; private set; }
    public int TotalVisitors { get; private set; }
    public int ScannedArtworks { get; private set; }
    public double AvgDuration { get; private set; }
    public int Nps { get; private set; }
}