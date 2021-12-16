var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<GeoPlaceIp.Infras.IRepo, DataRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
new DataLoader();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
