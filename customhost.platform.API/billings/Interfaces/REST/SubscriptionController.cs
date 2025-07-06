using System.Net.Mime;
using customhost_backend.billings.Domain.Services;
using customhost_backend.billings.Interfaces.REST.Resources;
using customhost_backend.billings.Interfaces.REST.Transform;
using customhost.platform.API.billings.Domain.Models.Queries;
using Microsoft.AspNetCore.Mvc;

namespace customhost_backend.billings.Interfaces.REST;

[ApiController]
[Route ("api/v1/[controller]")]
[Produces (MediaTypeNames.Application.Json)]
public class SubscriptionController(ISubscriptionCommandService subscriptionCommandService, ISubscriptionQueryService subscriptionQueryService): ControllerBase
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
    
    [HttpGet]
    public async Task<IActionResult> GetAllSubscriptions()
    {
        var subscription = await subscriptionQueryService.Handle(new GetAllSubscriptionsQuery());
        var resources = subscription.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
}