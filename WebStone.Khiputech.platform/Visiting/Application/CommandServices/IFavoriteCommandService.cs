using WebStone.Khiputech.Platform.Visiting.Domain.Model.Commands;

namespace WebStone.Khiputech.Platform.Visiting.Application.CommandServices;

public interface IFavoriteCommandService
{
    Task Handle(AddFavoriteCommand command);

    Task Handle(DeleteFavoriteCommand command);
}