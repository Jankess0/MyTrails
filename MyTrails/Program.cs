
using MongoDB.Driver;
using MyTrails.Mappers;
using MyTrails.Models;
using MyTrails.Services;
using MyTrails.Settings;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Konfiguracja MongoDB
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Rejestracja MongoClient
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration
        .GetSection("MongoDbSettings")
        .Get<MongoDbSettings>();

    return new MongoClient(settings.ConnectionString);
});

// Rejestracja kolekcji Trip
builder.Services.AddScoped<IMongoCollection<Trip>>(sp =>
{
    var settings = builder.Configuration
        .GetSection("MongoDbSettings")
        .Get<MongoDbSettings>();

    var client = sp.GetRequiredService<IMongoClient>();
    var database = client.GetDatabase(settings.DatabaseName);

    return database.GetCollection<Trip>(settings.TripsCollectionName);
});

builder.Services.AddScoped<TripService>();

builder.Services.AddSingleton<TripMapper>();
    

// 2️⃣ Kontrolery + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// 3️⃣ Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();
app.Run();