using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Domain.Repositories;
using customhost_backend.billings.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;
using customhost.platform.API.billings.Domain.Models.Queries;

namespace customhost_backend.billings.Application.Internal.QueryServices;

public class SubscriptionQueryService(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork) : ISubscriptionQueryService
{
    public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query)
    {
        return await subscriptionRepository.ListAsync();
    }
}