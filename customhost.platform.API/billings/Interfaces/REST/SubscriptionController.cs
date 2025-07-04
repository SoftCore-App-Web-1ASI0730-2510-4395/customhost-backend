using System.Net.Mime;
using customhost_backend.billings.Domain.Services;
using customhost_backend.billings.Interfaces.REST.Resources;
using customhost_backend.billings.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace customhost_backend.billings.Interfaces.REST;

[ApiController]
[Route ("api/v1/[controller]")]
[Produces (MediaTypeNames.Application.Json)]
public class SubscriptionController(ISubscriptionCommandService subscriptionCommandService): ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionResource resource)
    {
        var createSubscriptionCommand = CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var subscription = await subscriptionCommandService.Handle(createSubscriptionCommand);
        var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription);
        return StatusCode(201, subscriptionResource);
    }
    
}