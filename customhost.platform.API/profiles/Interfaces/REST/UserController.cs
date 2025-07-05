using System.Net.Mime;
using customhost_backend.IAM.Domain.Model.Queries;
using customhost_backend.IAM.Interfaces.REST.Transform;
using customhost_backend.profiles.Domain.Models.Commands;
using customhost_backend.profiles.Domain.Models.ValueObjects;
using customhost_backend.profiles.Domain.Services;
using customhost_backend.profiles.Interfaces.REST.Resources;
using customhost_backend.profiles.Interfaces.REST.Transform;
using customhost.platform.API.profiles.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace customhost_backend.profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Profiles")]
public class ProfilesController(
    IProfileCommandService ProfileCommandService,
    IProfileQueryService ProfileQueryService)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new Profile",
        Description = "Creates a new Profile with the specified details.",
        OperationId = "CreateProfile")]
    [SwaggerResponse(201, "Profile created successfully", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid profile data", null)]
    public async Task<ActionResult> CreateProfile(CreateProfileResource ProfileResource)
    {
        var command = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(ProfileResource);
        var result = await ProfileCommandService.Handle(command);
        if (result == null) return BadRequest("Profile could not be created. Please check the provided data.");
        var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(result);
        
        return StatusCode(201, resource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Profiles",
        Description = "Retrieves a list of all Profiles.",
        OperationId = "GetProfiles")]
    [SwaggerResponse(200, "Profiles retrieved successfully", typeof(IEnumerable<ProfileResource>))]
    public async Task<ActionResult> GetProfiles()
    {
        var Profiles = await ProfileQueryService.Handle(new GetAllProfilesQuery());
        var resources = Profiles.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get Profile by ID",
        Description = "Retrieves a specific Profile by their ID.",
        OperationId = "GetProfileById")]
    [SwaggerResponse(200, "Profile retrieved successfully", typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found", null)]
    public async Task<ActionResult> GetProfileById(int id)
    {
        var Profile = await ProfileQueryService.GetByIdAsync(id);
        if (Profile == null)
            return NotFound($"Profile with ID {id} not found.");

        var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(Profile);
        return Ok(resource);
    }

    [HttpGet("email/{email}")]
    [SwaggerOperation(
        Summary = "Get Profile by email",
        Description = "Retrieves a specific Profile by their email address.",
        OperationId = "GetProfileByEmail")]
    [SwaggerResponse(200, "Profile retrieved successfully", typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found", null)]
    public async Task<ActionResult> GetProfileByEmail(string email)
    {
        var Profile = await ProfileQueryService.GetByEmailAsync(email);
        if (Profile == null)
            return NotFound($"Profile with email {email} not found.");

        var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(Profile);
        return Ok(resource);
    }

    [HttpGet("hotel/{hotelId:int}")]
    [SwaggerOperation(
        Summary = "Get Profiles by hotel ID",
        Description = "Retrieves all Profiles for a specific hotel.",
        OperationId = "GetProfilesByHotelId")]
    [SwaggerResponse(200, "Profiles retrieved successfully", typeof(IEnumerable<ProfileResource>))]
    public async Task<ActionResult> GetProfilesByHotelId(int hotelId)
    {
        var Profiles = (await ProfileQueryService.GetByHotelIdAsync(hotelId)).ToList();
        var resources = ProfileResourceFromEntityAssembler.ToResourcesFromEntities(Profiles);
        return Ok(resources);
    }

    [HttpGet("role/{role}")]
    [SwaggerOperation(
        Summary = "Get Profiles by role",
        Description = "Retrieves all Profiles with a specific role.",
        OperationId = "GetProfilesByRole")]
    [SwaggerResponse(200, "Profiles retrieved successfully", typeof(IEnumerable<ProfileResource>))]
    [SwaggerResponse(400, "Invalid role value", null)]
    public async Task<ActionResult> GetProfilesByRole(string role)
    {
        if (!Enum.TryParse<EProfileRole>(role, true, out var roleEnum))
            return BadRequest($"Invalid role value: {role}");

        var Profiles = (await ProfileQueryService.GetByRoleAsync(roleEnum)).ToList();
        var resources = ProfileResourceFromEntityAssembler.ToResourcesFromEntities(Profiles);
        return Ok(resources);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update a Profile",
        Description = "Updates an existing Profile with new information.",
        OperationId = "UpdateProfile")]
    [SwaggerResponse(200, "Profile updated successfully", typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found", null)]
    [SwaggerResponse(400, "Profile update failed", null)]
    public async Task<ActionResult> UpdateProfile(int id, [FromBody] UpdateProfileResource updateResource)
    {
        var command = UpdateProfileCommandFromResourceAssembler.ToCommandFromResource(id, updateResource);
        var result = await ProfileCommandService.Handle(command);
        if (result == null)
            return NotFound($"Profile with ID {id} not found or email already exists.");

        var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete Profile",
        Description = "Deletes a Profile from the system.",
        OperationId = "DeleteProfile")]
    [SwaggerResponse(200, "Profile deleted successfully")]
    [SwaggerResponse(404, "Profile not found", null)]
    [SwaggerResponse(400, "Profile deletion failed", null)]
    public async Task<ActionResult> DeleteProfile(int id)
    {
        var command = new DeleteProfileCommand(id);
        var result = await ProfileCommandService.Handle(command);
        if (!result)
            return NotFound($"Profile with ID {id} not found.");

        return Ok($"Profile with ID {id} deleted successfully.");
    }
}