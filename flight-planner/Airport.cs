using System.Text.Json.Serialization;

namespace flight_planner;

public class Airport
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Country { get; set; }
    public string City { get; set; }

    [JsonPropertyName("airport")]
    public string AirportCode { get; set; }

    public override bool Equals(object obj)
    {
        var comparison = obj as Airport;
        return comparison != null &&
               Country == comparison.Country &&
               City == comparison.City &&
               AirportCode == comparison.AirportCode;
    }
}