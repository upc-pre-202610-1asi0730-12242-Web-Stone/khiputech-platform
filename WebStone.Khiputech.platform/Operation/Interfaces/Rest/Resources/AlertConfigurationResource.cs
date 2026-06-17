namespace WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Resources;

public record AlertConfigurationResource(
    int UmbralModerada,
    int UmbralCritica,
    bool NotificacionesCorreo,
    bool NotificacionesWhatsapp,
    bool NotificacionesSms,
    bool NotificacionesPanel,
    string ContactoDefensaCivil
);