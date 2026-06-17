namespace WebStone.Khiputech.Platform.Analytics.Interfaces.Rest.Resources;

public record RankingArtworkResource(
    int Id,
    string Nombre,
    string Sala,
    string Artista,
    int Engagement,
    double TiempoPromedio,
    string Trend
);