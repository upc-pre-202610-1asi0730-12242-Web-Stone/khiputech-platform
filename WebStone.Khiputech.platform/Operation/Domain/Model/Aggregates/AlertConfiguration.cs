namespace WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;

public class AlertConfiguration
{
    public AlertConfiguration() { }

    public AlertConfiguration(int moderateThreshold, int criticalThreshold, string contactCivilDefense)
    {
        ModerateThreshold = moderateThreshold;
        CriticalThreshold = criticalThreshold;
        ContactCivilDefense = contactCivilDefense;
        NotifyEmail = true;
        NotifyWhatsApp = false;
        NotifySms = false;
        NotifyPanel = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public int Id { get; private set; } = 1; // singleton
    public int ModerateThreshold { get; private set; } = 80;
    public int CriticalThreshold { get; private set; } = 95;
    public bool NotifyEmail { get; private set; } = true;
    public bool NotifyWhatsApp { get; private set; } = false;
    public bool NotifySms { get; private set; } = false;
    public bool NotifyPanel { get; private set; } = true;
    public string ContactCivilDefense { get; private set; } = string.Empty;
    public DateTime UpdatedAt { get; private set; }

    public void UpdateThresholds(int moderate, int critical)
    {
        ModerateThreshold = moderate;
        CriticalThreshold = critical;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateNotifications(bool email, bool whatsapp, bool sms, bool panel)
    {
        NotifyEmail = email;
        NotifyWhatsApp = whatsapp;
        NotifySms = sms;
        NotifyPanel = panel;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateContactCivilDefense(string contact)
    {
        ContactCivilDefense = contact;
        UpdatedAt = DateTime.UtcNow;
    }
}