namespace WebStone.Khiputech.Platform.Operation.Interfaces.Rest.Resources;

public record AlertResource(
    int Id,
    string Sala,
    string Tipo,
    string Mensaje,
    string Tiempo // formateado como "Hace X minutos"
);