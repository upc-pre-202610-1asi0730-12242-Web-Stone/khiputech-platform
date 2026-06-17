using WebStone.Khiputech.Platform.Visiting.Domain.Model.Commands;

namespace WebStone.Khiputech.Platform.Visiting.Application.CommandServices;

public interface IVisitHistoryCommandService
{
    Task Handle(RegisterVisitHistoryCommand command);
}