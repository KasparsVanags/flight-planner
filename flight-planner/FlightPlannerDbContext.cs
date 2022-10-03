using Microsoft.EntityFrameworkCore;

namespace flight_planner;

public class FlightPlannerDbContext : DbContext
{
    public FlightPlannerDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Flight> Flights { get; set; }
    public DbSet<Airport> Airports { get; set; }
}