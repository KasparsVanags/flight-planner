using AutoMapper;
using flight_planner.Models;
using FlightPlanner.Core.Models;

namespace flight_planner;

public class AutomapperConfig
{
    public static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<AirportRequest, Airport>()
                    .ForMember(d => d.Id, options => options.Ignore()
                    )
                    .ForMember(d => d.AirportCode, opt => opt.MapFrom(s => s.Airport));
                cfg.CreateMap<Airport, AirportRequest>()
                    .ForMember(d => d.Airport, opt => opt.MapFrom(s => s.AirportCode));
                cfg.CreateMap<FlightRequest, Flight>();
                cfg.CreateMap<Flight, FlightRequest>();
            });

        config.AssertConfigurationIsValid();
        return config.CreateMapper();
    }
}