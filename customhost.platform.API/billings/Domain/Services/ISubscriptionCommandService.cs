using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Domain.Models.Commands;

namespace customhost_backend.billings.Domain.Services;

public interface ISubscriptionCommandService
{
    Task<Subscription?> Handle(CreateSubscriptionCommand command);
}