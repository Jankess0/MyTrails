using MyTrails.DTOs;
using MyTrails.Models;
using Riok.Mapperly.Abstractions;

namespace MyTrails.Mappers;

[Mapper]
public partial class TripMapper
{
    public partial TripDto ToDto(Trip trip);
    public partial Trip ToEntity(TripDto dto);
    public partial void UpdateEntity(TripDto dto, Trip trip);
}