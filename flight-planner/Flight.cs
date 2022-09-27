namespace flight_planner;

public class Flight
{
    public int Id { get; set; }
    public Airport From { get; set; }
    public Airport To { get; set; }
    public string Carrier { get; set; }
    public string DepartureTime { get; set; }
    public string ArrivalTime { get; set; }

    public override bool Equals(object obj)
    {
        var comparison = obj as Flight;
        return comparison != null &&
               From.Equals(comparison.From) &&
               To.Equals(comparison.To) &&
               Carrier == comparison.Carrier &&
               DepartureTime == comparison.DepartureTime &&
               ArrivalTime == comparison.ArrivalTime;
    }
}