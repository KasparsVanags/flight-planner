using flight_planner.Models;

namespace FlightPlanner.Core.Models.SearchFlightsRequestValidators;

public static class RequestFormatter
{
    public static SearchFlightsRequest Format(this SearchFlightsRequest request)
    {
        request.From = request.From.Trim().ToUpper();
        request.To = request.To.Trim().ToUpper();
        
        return request;
    }
}