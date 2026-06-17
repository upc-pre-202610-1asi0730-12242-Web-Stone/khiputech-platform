namespace WebStone.Khiputech.Platform.Analytics.Interfaces.Rest.Resources;

public record SponsorResource(
    int Id,
    string Nombre,
    string Sala,
    int Impresiones,
    decimal Aportado,
    string Estado  
);