using WebStone.Khiputech.Platform.Operation.Application.CommandServices;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Aggregates;
using WebStone.Khiputech.Platform.Operation.Domain.Model.Commands;
using WebStone.Khiputech.Platform.Operation.Domain.Repositories;
using WebStone.Khiputech.Platform.Shared.Domain.Repositories;

namespace WebStone.Khiputech.Platform.Operation.Application.Internal.CommandServices;

public class OperationCommandService(
    IAlertRepository alertRepository,
    IAlertConfigurationRepository configRepository,
    IOperationRecommendationRepository recommendationRepository,  // ← Agregar
    IUnitOfWork unitOfWork) : IOperationCommandService
{
    public async Task Handle(CreateAlertCommand command, CancellationToken ct)
    {
        var alert = new Alert(command.RoomName, command.Type, command.Message, command.TriggeredBy);
        await alertRepository.AddAsync(alert, ct);
        await unitOfWork.CompleteAsync(ct);
    }

    public async Task Handle(ResolveAlertCommand command, CancellationToken ct)
    {
        var alert = await alertRepository.FindByIdAsync(command.AlertId, ct);
        if (alert == null) throw new Exception("Alert not found");
        alert.Resolve();
        alertRepository.Update(alert);
        await unitOfWork.CompleteAsync(ct);
    }

    public async Task Handle(UpdateAlertConfigurationCommand command, CancellationToken ct)
    {
        var config = await configRepository.GetAsync(ct);
        if (config == null)
            throw new Exception("Configuration not found");
        config.UpdateThresholds(command.ModerateThreshold, command.CriticalThreshold);
        config.UpdateNotifications(command.NotifyEmail, command.NotifyWhatsApp, command.NotifySms, command.NotifyPanel);
        config.UpdateContactCivilDefense(command.ContactCivilDefense);
        configRepository.Update(config);
        await unitOfWork.CompleteAsync(ct);
    }
    
    public async Task<OperationRecommendation> Handle(CreateRecommendationCommand command, CancellationToken ct)
    {
        var recommendation = new OperationRecommendation(
            command.RoomName,
            command.Issue,
            command.SuggestedAction
        );
        await recommendationRepository.AddAsync(recommendation, ct);  // ← Usar el parámetro, no _recommendationRepository
        await unitOfWork.CompleteAsync(ct);  // ← Usar el parámetro, no _unitOfWork
        return recommendation;
    }
}