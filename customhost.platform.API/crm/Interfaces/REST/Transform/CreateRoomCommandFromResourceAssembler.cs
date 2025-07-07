using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform create room resource to create room command
/// </summary>
public static class CreateRoomCommandFromResourceAssembler
{
    /// <summary>
    /// Transform create room resource to create room command
    /// </summary>
    /// <param name="resource">Create room resource</param>
    /// <returns>Create room command</returns>
    public static CreateRoomCommand ToCommandFromResource(CreateRoomResource resource)
    {
        // Parse enums from string
        if (!Enum.TryParse<ERoomStatus>(resource.Status, out var status))
            throw new ArgumentException($"Invalid room status: {resource.Status}");
            
        if (!Enum.TryParse<ERoomType>(resource.Type, out var type))
            throw new ArgumentException($"Invalid room type: {resource.Type}");

        return new CreateRoomCommand(
            resource.RoomNumber,
            status,
            type,
            resource.HotelId,
            resource.Price,
            resource.Floor
        );
    }
}