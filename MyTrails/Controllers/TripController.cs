using Microsoft.AspNetCore.Mvc;
using MyTrails.DTOs;
using MyTrails.Services;

namespace MyTrails.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
    private readonly TripService _tripService;

    public TripController(TripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripDto>>> GetAll()
    {
        var trips = await _tripService.GetAllTripsAsync();
        return Ok(trips);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TripDto>> GetById(string id)
    {
        var trip = await _tripService.GetTripAsync(id);
        if (trip == null) 
            return NotFound();
        return Ok(trip);
    }

    [HttpPost]
    public async Task<ActionResult<TripDto>> Create(TripDto dto)
    {
        var trip = await _tripService.CreateTripAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = trip.Id }, trip);
    }
}