using System.Net.Mime;
using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Domain.Model.Queries;
using customhost_backend.GuestExperience.Domain.Services;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;
using customhost_backend.GuestExperience.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace customhost_backend.GuestExperience.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Room Device Preference Endpoints")]
public class DevicePreferencesController(
    IDevicePreferenceCommandService DevicePreferenceCommandService,
    IDevicePreferenceQueryService DevicePreferenceQueryService
) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all room device preferences",
        Description = "Get all room device preferences.",
        OperationId = "GetAllDevicePreferences")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room device preferences found", typeof(IEnumerable<DevicePreferenceResource>))]
    public async Task<IActionResult> GetAllDevicePreferences()
    {
        var getAllDevicePreferencesQuery = new GetAllDevicePreferencesQuery();
        var preferences = await DevicePreferenceQueryService.Handle(getAllDevicePreferencesQuery);
        var preferenceResources = preferences.Select(DevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(preferenceResources);
    }

    [HttpGet("{preferenceId:int}")]
    [SwaggerOperation(
        Summary = "Gets a room device preference by its ID",
        Description = "Get a room device preference by given preference ID.",
        OperationId = "GetDevicePreferenceById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room device preference found", typeof(DevicePreferenceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Room device preference not found")]
    public async Task<IActionResult> GetDevicePreferenceById([FromRoute] int preferenceId)
    {
        var getDevicePreferenceByIdQuery = new GetDevicePreferenceByIdQuery(preferenceId);
        var preference = await DevicePreferenceQueryService.Handle(getDevicePreferenceByIdQuery);
        if (preference is null) return NotFound();
        var preferenceResource = DevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity(preference);
        return Ok(preferenceResource);
    }

    [HttpGet("room-device/{DeviceId:int}")]
    [SwaggerOperation(
        Summary = "Gets preferences for a specific room device",
        Description = "Get all preferences for a specific room device.",
        OperationId = "GetDevicePreferencesByDeviceId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room device preferences found", typeof(IEnumerable<DevicePreferenceResource>))]
    public async Task<IActionResult> GetDevicePreferencesByDeviceId([FromRoute] int DeviceId)
    {
        var getDevicePreferencesByDeviceIdQuery = new GetDevicePreferencesByDeviceIdQuery(DeviceId);
        var preferences = await DevicePreferenceQueryService.Handle(getDevicePreferencesByDeviceIdQuery);
        var preferenceResources = preferences.Select(DevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(preferenceResources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates new room device preferences",
        Description = "Creates preferences for a specific room device.",
        OperationId = "CreateDevicePreference")]
    [SwaggerResponse(StatusCodes.Status201Created, "Room device preference created successfully", typeof(DevicePreferenceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid room device preference data")]
    public async Task<IActionResult> CreateDevicePreference([FromBody] CreateDevicePreferenceResource resource)
    {
        var createDevicePreferenceCommand = CreateDevicePreferenceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var preference = await DevicePreferenceCommandService.Handle(createDevicePreferenceCommand);
        if (preference is null) return BadRequest("Room device preference could not be created.");
        var preferenceResource = DevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity(preference);
        return CreatedAtAction(nameof(GetDevicePreferenceById), new { preferenceId = preference.Id }, preferenceResource);
    }

    [HttpPut("{preferenceId:int}")]
    [SwaggerOperation(
        Summary = "Updates room device preferences",
        Description = "Updates existing room device preferences.",
        OperationId = "UpdateDevicePreference")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room device preference updated successfully", typeof(DevicePreferenceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Room device preference not found")]
    public async Task<IActionResult> UpdateDevicePreference([FromRoute] int preferenceId, [FromBody] UpdateDevicePreferenceResource resource)
    {
        var updateDevicePreferenceCommand = new UpdateDevicePreferenceCommand(preferenceId, resource.DeviceId, resource.Preferences);
        var preference = await DevicePreferenceCommandService.Handle(updateDevicePreferenceCommand);
        if (preference is null) return NotFound();
        var preferenceResource = DevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity(preference);
        return Ok(preferenceResource);
    }
}
