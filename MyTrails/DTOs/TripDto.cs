namespace MyTrails.DTOs;

public class TripDto
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    //public List<GeoPoinst> Route { get; set; }
}