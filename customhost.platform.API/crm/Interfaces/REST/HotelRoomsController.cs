using System.Net.Mime;
using customhost_backend.crm.Domain.Models.Queries;
using customhost_backend.crm.Domain.Services;
using customhost_backend.crm.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace customhost_backend.crm.Interfaces.REST;



[ApiController]
[Route("api/v1/hotel/{hotelId:int}/rooms")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Hotel")]
public class HotelRoomsController(IRoomQueryService roomQueryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRoomsByHotel(
        [FromRoute] int hotelId)
    {
        var getAllRoomsByHotelIdQuery = new GetAllRoomsByHotelIdQuery(hotelId);
        var rooms = await roomQueryService.Handle(getAllRoomsByHotelIdQuery);
        var roomResources = rooms.Select(RoomResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roomResources);
    }
}