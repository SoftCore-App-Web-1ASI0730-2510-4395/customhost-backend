using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Domain.Models.Commands;
using customhost_backend.billings.Domain.Repositories;
using customhost_backend.billings.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;
namespace customhost_backend.billings.Application.Internal.CommandServices;

public class SubscriptionCommandService(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork, ISubscriptionPlanRepository subscriptionPlanRepository) : ISubscriptionCommandService
{
    public async Task<Subscription?> Handle(CreateSubscriptionCommand command)
    {

        var alreadyExists = await subscriptionRepository.ExistsByHotelIdAndSubscriptionPlanIdAndActive(
            command.HotelId, command.SubscriptionPlanId);
        if (alreadyExists)
        {
            throw new Exception($"There is already an active subscription for hotel ID {command.HotelId} with subscription plan ID {command.SubscriptionPlanId}.");
        }
        
        var subscriptionPlan = await subscriptionPlanRepository.FindByIdAsync(command.SubscriptionPlanId);
        if (subscriptionPlan is null) throw new Exception($"Subscription plan with ID {command.SubscriptionPlanId} not found.");
        var Subscription = new Subscription(command);
        await subscriptionRepository.AddAsync(Subscription);
        await unitOfWork.CompleteAsync();
       
        return Subscription;
    }
}