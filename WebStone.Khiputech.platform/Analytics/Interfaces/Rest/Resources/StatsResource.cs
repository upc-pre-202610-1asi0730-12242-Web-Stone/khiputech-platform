namespace WebStone.Khiputech.Platform.Analytics.Interfaces.Rest.Resources;

public record StatsResource(
    int VisitantesHoy,
    int VisitantesChange,
    int ObrasEscaneadas,
    int ObrasChange,
    int DuracionMedia,
    int DuracionChange,
    int Nps,
    int NpsChange
);