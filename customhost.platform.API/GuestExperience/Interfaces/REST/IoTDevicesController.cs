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
[SwaggerTag("Available IoT Device Endpoints")]
public class DeviceModelsController(
    IDeviceModelCommandService DeviceModelCommandService,
    IDeviceModelQueryService DeviceModelQueryService
) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all IoT devices",
        Description = "Get all available IoT devices.",
        OperationId = "GetAllDeviceModels")]
    [SwaggerResponse(StatusCodes.Status200OK, "IoT devices found", typeof(IEnumerable<DeviceModelResource>))]
    public async Task<IActionResult> GetAllDeviceModels()
    {
        var getAllDeviceModelsQuery = new GetAllDeviceModelsQuery();
        var DeviceModels = await DeviceModelQueryService.Handle(getAllDeviceModelsQuery);
        var DeviceModelResources = DeviceModels.Select(DeviceModelResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(DeviceModelResources);
    }

    [HttpGet("{DeviceModelId:int}")]
    [SwaggerOperation(
        Summary = "Gets an IoT device by its ID",
        Description = "Get an IoT device by given device ID.",
        OperationId = "GetDeviceModelById")]
    [SwaggerResponse(StatusCodes.Status200OK, "IoT device found", typeof(DeviceModelResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "IoT device not found")]
    public async Task<IActionResult> GetDeviceModelById([FromRoute] int DeviceModelId)
    {
        var getDeviceModelByIdQuery = new GetDeviceModelByIdQuery(DeviceModelId);
        var DeviceModel = await DeviceModelQueryService.Handle(getDeviceModelByIdQuery);
        if (DeviceModel is null) return NotFound();
        var DeviceModelResource = DeviceModelResourceFromEntityAssembler.ToResourceFromEntity(DeviceModel);
        return Ok(DeviceModelResource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a new IoT device",
        Description = "Creates a new IoT device with the provided details.",
        OperationId = "CreateDeviceModel")]
    [SwaggerResponse(StatusCodes.Status201Created, "IoT device created successfully", typeof(DeviceModelResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid IoT device data")]
    public async Task<IActionResult> CreateDeviceModel([FromBody] CreateDeviceModelResource resource)
    {
        var createDeviceModelCommand = CreateDeviceModelCommandFromResourceAssembler.ToCommandFromResource(resource);
        var DeviceModel = await DeviceModelCommandService.Handle(createDeviceModelCommand);
        if (DeviceModel is null) return BadRequest("IoT device could not be created.");
        var DeviceModelResource = DeviceModelResourceFromEntityAssembler.ToResourceFromEntity(DeviceModel);
        return CreatedAtAction(nameof(GetDeviceModelById), new { DeviceModelId = DeviceModel.Id }, DeviceModelResource);
    }

    [HttpPut("{DeviceModelId:int}")]
    [SwaggerOperation(
        Summary = "Updates an existing IoT device",
        Description = "Updates an existing IoT device with the provided details.",
        OperationId = "UpdateDeviceModel")]
    [SwaggerResponse(StatusCodes.Status200OK, "IoT device updated successfully", typeof(DeviceModelResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "IoT device not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid IoT device data")]
    public async Task<IActionResult> UpdateDeviceModel([FromRoute] int DeviceModelId, [FromBody] UpdateDeviceModelResource resource)
    {
        var updateDeviceModelCommand = UpdateDeviceModelCommandFromResourceAssembler.ToCommandFromResource(DeviceModelId, resource);
        var DeviceModel = await DeviceModelCommandService.Handle(updateDeviceModelCommand);
        if (DeviceModel is null) return NotFound();
        var DeviceModelResource = DeviceModelResourceFromEntityAssembler.ToResourceFromEntity(DeviceModel);
        return Ok(DeviceModelResource);
    }

    [HttpDelete("{DeviceModelId:int}")]
    [SwaggerOperation(
        Summary = "Deletes an IoT device",
        Description = "Deletes an existing IoT device by its ID.",
        OperationId = "DeleteDeviceModel")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "IoT device deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "IoT device not found")]
    public async Task<IActionResult> DeleteDeviceModel([FromRoute] int DeviceModelId)
    {
        var deleteDeviceModelCommand = new DeleteDeviceModelCommand(DeviceModelId);
        var result = await DeviceModelCommandService.Handle(deleteDeviceModelCommand);
        if (!result) return NotFound();
        return NoContent();
    }
}
