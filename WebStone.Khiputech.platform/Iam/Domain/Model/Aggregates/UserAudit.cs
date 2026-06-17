using WebStone.Khiputech.Platform.Shared.Domain.Model.Entities;

namespace WebStone.Khiputech.Platform.Iam.Domain.Model.Aggregates;


public partial class User : IAuditableEntity
{

    public DateTimeOffset? CreatedAt { get; set; }


    public DateTimeOffset? UpdatedAt { get; set; }
}