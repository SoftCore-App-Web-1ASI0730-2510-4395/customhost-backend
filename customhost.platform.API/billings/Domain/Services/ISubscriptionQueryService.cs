using customhost_backend.billings.Domain.Models.Aggregates;
using customhost.platform.API.billings.Domain.Models.Queries;

namespace customhost_backend.billings.Domain.Services;

public interface ISubscriptionQueryService
{
    Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query);
}