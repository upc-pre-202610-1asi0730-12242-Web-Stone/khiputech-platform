using WebStone.Khiputech.Platform.Capacity.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;

public class OperationRecommendation
{
    public int Id { get; set; }
    public string RoomName { get; set; }
    public string Issue { get; set; }
    public string SuggestedAction { get; set; }
    public DateTime GeneratedAt { get; set; }

    public OperationRecommendation() { }

    public OperationRecommendation(string roomName, string issue, string suggestedAction)
    {
        RoomName = roomName;
        Issue = issue;
        SuggestedAction = suggestedAction;
        GeneratedAt = DateTime.UtcNow;
    }
}