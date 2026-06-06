namespace WebStone.Khiputech.Platform.Analytics.Interfaces.Rest.Resources;

public record TopArtworkResource(
    int Id,
    string Nombre,
    string Sala,
    int Visitas,
    int Retencion,
    string Estado
);