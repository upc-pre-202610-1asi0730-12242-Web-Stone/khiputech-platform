using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Resources;

namespace WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Transform;

public static class AlertResourceFromEntityAssembler
{
    public static AlertResource ToResourceFromEntity(Alert alert)
    {
        var timeAgo = DateTime.UtcNow - alert.CreatedAt;
        string tiempo = timeAgo.TotalMinutes < 1 ? "Hace unos segundos" :
            timeAgo.TotalMinutes < 60 ? $"Hace {(int)timeAgo.TotalMinutes} minutos" :
            $"Hace {(int)timeAgo.TotalHours} horas";
        return new AlertResource(
            alert.Id,
            alert.RoomName,
            alert.Type == "moderada" ? "Alerta" : "Crítica",
            alert.Message,
            tiempo
        );
    }
}