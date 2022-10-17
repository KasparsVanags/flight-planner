namespace flight_planner.Models;

public class SearchFlightsRequest
{
    public string From { get; set; }
    public string To { get; set; }
    public string DepartureDate { get; set; }
}