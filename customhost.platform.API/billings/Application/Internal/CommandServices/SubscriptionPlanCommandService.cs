using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Domain.Models.Commands;
using customhost_backend.billings.Domain.Repositories;
using customhost_backend.billings.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.billings.Application.Internal.CommandServices;

public class SubscriptionPlanCommandService(ISubscriptionPlanRepository subscriptionPlanRepository, IUnitOfWork unitOfWork) 
    : ISubscriptionPlanCommandService
{
    public async Task<SubscriptionPlan?> Handle(CreateSubscriptionPlanCommand command)
    {
        var SubscriptionPlan = new SubscriptionPlan(command);
        await subscriptionPlanRepository.AddAsync(SubscriptionPlan);
        await unitOfWork.CompleteAsync();
        return SubscriptionPlan;
    }
}