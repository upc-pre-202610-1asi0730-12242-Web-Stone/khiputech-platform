using WebStone.Khiputech.Platform.Iam.Domain.Model.Aggregates;

namespace WebStone.Khiputech.Platform.Iam.Application.Internal.OutboundServices;

public interface ITokenService
{

    string GenerateToken(User user);


    Task<int?> ValidateToken(string token);
}