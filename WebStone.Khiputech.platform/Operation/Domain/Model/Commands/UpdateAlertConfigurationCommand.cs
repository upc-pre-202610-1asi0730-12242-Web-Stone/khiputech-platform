namespace WebStone.Khiputech.Platform.Operation.Domain.Model.Commands;

public record UpdateAlertConfigurationCommand(
    int ModerateThreshold,
    int CriticalThreshold,
    bool NotifyEmail,
    bool NotifyWhatsApp,
    bool NotifySms,
    bool NotifyPanel,
    string ContactCivilDefense);