namespace WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Resources;

public record UpdateAlertConfigurationRequest(
    int ModerateThreshold,
    int CriticalThreshold,
    bool NotifyEmail,
    bool NotifyWhatsApp,
    bool NotifySms,
    bool NotifyPanel,
    string ContactCivilDefense
);