using flight_planner;
using flight_planner.Filters;
using flight_planner.Models.SearchFlightsRequestValidators;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Models.AirportValidators;
using FlightPlanner.Core.Models.FlightValidators;
using FlightPlanner.Core.Models.SearchFlightsRequestValidators;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthorizationHandler>("BasicAuthentication", null);
builder.Services.AddDbContext<FlightPlannerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("flight-planner"));
});

builder.Services.AddScoped<IFlightPlannerDbContext, FlightPlannerDbContext>();
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();
builder.Services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddSingleton(AutomapperConfig.CreateMapper());

builder.Services.AddScoped<IFlightValidator, FlightAirportValidator>();
builder.Services.AddScoped<IFlightValidator, FlightCarrierValidator>();
builder.Services.AddScoped<IFlightValidator, FlightTimeValidator>();

builder.Services.AddScoped<IAirportValidator, AirportCityValidator>();
builder.Services.AddScoped<IAirportValidator, AirportCountryValidator>();
builder.Services.AddScoped<IAirportValidator, AirportCodeValidator>();

builder.Services.AddScoped<ISearchFlightsRequestValidator, RequestAirportValidator>();
builder.Services.AddScoped<ISearchFlightsRequestValidator, RequestDepartureDateValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();