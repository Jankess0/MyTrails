using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrails.Models;

public class Trip
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Duration { get; set; }
    public List<string> Tags { get; set; }
   // public List<GeoPoint> Route { get; set; } 
}