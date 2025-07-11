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
    public async Task<ActionResult<TripDto>> GetById(int id)
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

    [HttpPut("{id}")]
    public async Task<ActionResult<TripDto>> Update(int id, TripDto dto)
    {
        var trip = await _tripService.GetTripAsync(id);
        if (trip == null)
            return NotFound();
        
        trip.Name = dto.Name;
        trip.Description = dto.Description;
        trip.Duration = dto.Duration;
        await _tripService.UpdateTripAsync(trip);
        return Ok(trip);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<TripDto>> Delete(int id)
    {
        var trip = await _tripService.GetTripAsync(id);
        
        if (trip == null)
            return NotFound();
        
        await _tripService.DeleteTripAsync(id);
        return Ok(trip);
    }
}