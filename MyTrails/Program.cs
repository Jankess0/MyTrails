
using MyTrails.Settings;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Konfiguracja MongoDB
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

//builder.Services.AddSingleton<TripService>();

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