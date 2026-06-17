using WebStone.Khiputech.Platform.Visiting.Application.CommandServices;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Visiting.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Visiting.Domain.Repositories;

namespace WebStone.Khiputech.Platform.Visiting.Application.Internal.CommandServices;

public class VisitHistoryCommandService(IVisitHistoryRepository visitHistoryRepository)
    : IVisitHistoryCommandService
{
    public async Task Handle(RegisterVisitHistoryCommand command)
    {
        var visitHistory = new VisitHistory
        {
            ArtworkId = command.ArtworkId,
            SessionId = command.SessionId
        };

        await visitHistoryRepository.AddAsync(visitHistory);
    }
}