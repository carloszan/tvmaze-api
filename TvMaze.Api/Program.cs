using MongoDB.Driver;
using StackExchange.Redis;
using TvMaze.Api;
using TvMaze.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
  var settings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
  return new MongoClient(settings.ConnectionString);
});

var redisSettings = builder.Configuration.GetSection(nameof(RedisDbSettings)).Get<RedisDbSettings>();
var multiplexer = ConnectionMultiplexer.Connect(redisSettings.ConnectionString);
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

builder.Services.AddSingleton<IShowRepository, ShowRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
