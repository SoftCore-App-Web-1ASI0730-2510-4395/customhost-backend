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
public class SubscriptionPlanController(ISubscriptionPlanCommandService SubscriptionPlanCommandService, ISubscriptionPlanQueryService subscriptionPlanQueryService): ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreateSubscriptionPlan(CreateSubscriptionPlanResource resource)
    {
        var createSubscriptionPlanCommand = CreateSubscriptionPlanCommandFromResourceAssembler.ToCommandFromResource(resource);
        var SubscriptionPlan = await SubscriptionPlanCommandService.Handle(createSubscriptionPlanCommand);
        var SubscriptionPlanResource = SubscriptionPlanResourceFromEntityAssembler.ToResourceFromEntity(SubscriptionPlan);
        return StatusCode(201, SubscriptionPlanResource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSubscriptionPlans()
    {
        var subscriptionPlans = await subscriptionPlanQueryService.Handle(new GetAllSubscriptionPlansQuery());
        var resources = subscriptionPlans.Select(SubscriptionPlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
}