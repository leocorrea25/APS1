using Application.Ports;
using Application.Guest;
using Data;
using Data.Guest;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region
builder.Services.AddScoped<IGuestManager, GuestManager>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();
#endregion

#region

var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<HotelDbContext>(
    options =>options.UseSqlServer(connectionString));

#endregion




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
