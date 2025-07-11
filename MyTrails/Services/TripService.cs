using MongoDB.Driver;
using MyTrails.DTOs;
using MyTrails.Mappers;
using MyTrails.Models;

namespace MyTrails.Services;

public class TripService
{
    private readonly IMongoCollection<Trip> _trips;
    private readonly TripMapper _tripMapper;

    public TripService(IMongoCollection<Trip> trips, TripMapper tripMapper)
    {
        _trips = trips;
        _tripMapper = tripMapper;
    }

    public async Task<IEnumerable<TripDto>> GetAllTripsAsync()
    {
        var trips = await _trips.Find(_ => true).ToListAsync();
        return trips.Select(_tripMapper.ToDto);
    }

    public async Task<TripDto?> GetTripAsync(int id)
    {
        var trip = await _trips.Find(t => t.Id == id).FirstOrDefaultAsync();
        return trip == null ? null : _tripMapper.ToDto(trip);
    }

    public async Task<TripDto> CreateTripAsync(TripDto dto)
    {
        var lastTrip = await _trips
            .Find(_ => true)
            .SortByDescending(t => t.Id)
            .Limit(1)
            .FirstOrDefaultAsync();
          
        var nextId = (lastTrip?.Id ?? 0) + 1;

        var trip = _tripMapper.ToEntity(dto);
        trip.Id = nextId;
        await _trips.InsertOneAsync(trip);
        return _tripMapper.ToDto(trip);
        
    }

    public async Task<bool> UpdateTripAsync(TripDto dto)
    {
        var trip = await _trips.Find(t => t.Id == dto.Id).FirstOrDefaultAsync();
        if (trip == null) return false;
        
        _tripMapper.UpdateEntity(dto, trip);
        await _trips.ReplaceOneAsync(t => t.Id == dto.Id, trip);
        return true;
        
    }

    public async Task<bool> DeleteTripAsync(int id)
    {
        var trip = await _trips.Find(t => t.Id == id).FirstOrDefaultAsync();
        if (trip == null) return false;
        
        await _trips.DeleteOneAsync(t => t.Id == id);
        return true;
    }
    
    
}