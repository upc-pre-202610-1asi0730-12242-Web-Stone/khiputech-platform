using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Transform;

public static class AlertConfigurationResourceFromEntityAssembler
{
    public static AlertConfigurationResource ToResourceFromEntity(AlertConfiguration config)
    {
        return new AlertConfigurationResource(
            config.ModerateThreshold,
            config.CriticalThreshold,
            config.NotifyEmail,
            config.NotifyWhatsApp,
            config.NotifySms,
            config.NotifyPanel,
            config.ContactCivilDefense
        );
    }
}