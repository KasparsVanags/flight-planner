namespace FlightPlanner.Core.Models;

public class PageResult
{
    public List<Flight>? Items { get; set; }
    public int Page { get; set; }
    public int TotalItems => Items?.Count ?? 0;
}