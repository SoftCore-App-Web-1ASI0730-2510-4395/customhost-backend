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
[SwaggerTag("Available Room Device Endpoints")]
public class DevicesController(
    IDeviceCommandService DeviceCommandService,
    IDeviceQueryService DeviceQueryService
) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all room devices",
        Description = "Get all room devices with their IoT device details.",
        OperationId = "GetAllDevices")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room devices found", typeof(IEnumerable<DeviceResource>))]
    public async Task<IActionResult> GetAllDevices()
    {
        var getAllDevicesQuery = new GetAllDevicesQuery();
        var Devices = await DeviceQueryService.Handle(getAllDevicesQuery);
        var DeviceResources = Devices.Select(DeviceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(DeviceResources);
    }

    [HttpGet("{DeviceId:int}")]
    [SwaggerOperation(
        Summary = "Gets a room device by its ID",
        Description = "Get a room device by given device ID.",
        OperationId = "GetDeviceById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room device found", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Room device not found")]
    public async Task<IActionResult> GetDeviceById([FromRoute] int DeviceId)
    {
        var getDeviceByIdQuery = new GetDeviceByIdQuery(DeviceId);
        var Device = await DeviceQueryService.Handle(getDeviceByIdQuery);
        if (Device is null) return NotFound();
        var DeviceResource = DeviceResourceFromEntityAssembler.ToResourceFromEntity(Device);
        return Ok(DeviceResource);
    }

    [HttpGet("room/{roomId:int}")]
    [SwaggerOperation(
        Summary = "Gets all devices in a specific room",
        Description = "Get all devices assigned to a specific room.",
        OperationId = "GetDevicesByRoomId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room devices found", typeof(IEnumerable<DeviceResource>))]
    public async Task<IActionResult> GetDevicesByRoomId([FromRoute] int roomId)
    {
        var getDevicesByRoomIdQuery = new GetDevicesByRoomIdQuery(roomId);
        var Devices = await DeviceQueryService.Handle(getDevicesByRoomIdQuery);
        var DeviceResources = Devices.Select(DeviceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(DeviceResources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a new room device assignment",
        Description = "Assigns an IoT device to a specific room.",
        OperationId = "CreateDevice")]
    [SwaggerResponse(StatusCodes.Status201Created, "Room device created successfully", typeof(DeviceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid room device data")]
    public async Task<IActionResult> CreateDevice([FromBody] CreateDeviceResource resource)
    {
        var createDeviceCommand = CreateDeviceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var Device = await DeviceCommandService.Handle(createDeviceCommand);
        if (Device is null) return BadRequest("Room device could not be created.");
        var DeviceResource = DeviceResourceFromEntityAssembler.ToResourceFromEntity(Device);
        return CreatedAtAction(nameof(GetDeviceById), new { DeviceId = Device.Id }, DeviceResource);
    }
    
    
    
    [HttpPatch("{DeviceId:int}")]
    [SwaggerOperation(
        Summary = "Change device status",
        Description = "Change device status with device Id",
        OperationId = "ChangeDeviceStatus")]
    [SwaggerResponse(200, "Device status changed successfully", typeof(DeviceResource))]
    [SwaggerResponse(404, "Device not found", null)]
    [SwaggerResponse(400, "Device status changed failed", null)]
    public async Task<ActionResult> ChangeDeviceStatus([FromRoute] int DeviceId, [FromBody] ChangeDeviceStatusResource resource)
    {
        var command = new ChangeStatusDeviceCommand(DeviceId, resource.Status);
        var result = await DeviceCommandService.Handle(command);
        if (result == null)
            return NotFound($"Device with ID {DeviceId} not found.");

        var deviceResource = DeviceResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(deviceResource);
    }

    [HttpDelete("{DeviceId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a room device assignment",
        Description = "Removes an IoT device assignment from a room.",
        OperationId = "DeleteDevice")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Room device deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Room device not found")]
    public async Task<IActionResult> DeleteDevice([FromRoute] int DeviceId)
    {
        var deleteDeviceCommand = new DeleteDeviceCommand(DeviceId);
        var result = await DeviceCommandService.Handle(deleteDeviceCommand);
        if (!result) return NotFound();
        return NoContent();
    }
}
