using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace customhost_backend.billings.Infrastructure.Persistence.EFC.Repositories;

public class SubscriptionPlanRepository(AppDbContext context)
    : BaseRepository<SubscriptionPlan>(context), ISubscriptionPlanRepository
{

}