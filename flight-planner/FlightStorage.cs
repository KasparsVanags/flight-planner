namespace flight_planner;

public static class FlightStorage
{
    private static readonly object FlightLock = new();
    private static readonly List<Flight> Flights = new();
    private static readonly List<Airport> Airports = new();
    private static int _id = 1;

    public static Flight? AddFlight(Flight flight)
    {
        lock (FlightLock)
        {
            if (Flights.Any(x => x.Equals(flight))) return null;

            if (!Airports.Any(x => x.Equals(flight.From))) Airports.Add(flight.From);
            if (!Airports.Any(x => x.Equals(flight.To))) Airports.Add(flight.To);

            flight.Id = _id++;
            Flights.Add(flight);
        }

        return flight;
    }

    public static Flight? GetFlightById(int id)
    {
        return Flights.FirstOrDefault(x => x.Id == id);
    }

    public static List<Airport> GetAirport(string phrase)
    {
        phrase = phrase.Trim().ToLower();
        return Airports.Where(x => x.AirportCode.ToLower().Contains(phrase) ||
                                   x.City.ToLower().Contains(phrase) ||
                                   x.Country.ToLower().Contains(phrase)).ToList();
    }

    public static List<Flight> GetFlights(SearchFlightsRequest search)
    {
        return Flights.Where(x => x.From.AirportCode == search.From &&
                                  x.To.AirportCode == search.To &&
                                  DateTime.Parse(x.DepartureTime).Date == DateTime.Parse(search.DepartureDate).Date)
            .ToList();
    }

    public static void Clear()
    {
        lock (FlightLock)
        {
            Flights.Clear();
            _id = 1;
        }
    }

    public static void DeleteFlight(int id)
    {
        lock (FlightLock)
        {
            Flights.RemoveAll(x => x.Id == id);
        }
    }
}