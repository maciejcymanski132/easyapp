using Airtrafic.Data;
using Airtrafic.Services;
using Microsoft.EntityFrameworkCore;

namespace Airtrafic.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AirtraficContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<AirtraficContext>>()))
        {
            var Boeing = new AircraftManufacturer { AircraftManufacturerId = Guid.NewGuid(), Name = "Boeing" };
            var LockheedMartin = new AircraftManufacturer { AircraftManufacturerId = Guid.NewGuid(), Name = "Lockheed Martin" };
            var Airbus = new AircraftManufacturer { AircraftManufacturerId = Guid.NewGuid(), Name = "Airbus" };
            var Embraer = new AircraftManufacturer { AircraftManufacturerId = Guid.NewGuid(), Name = "Embraer" };


            if (!context.AircraftManufacturer.Any())
            {
                context.AircraftManufacturer.AddRange(Boeing, LockheedMartin, Airbus, Embraer);
            }
            context.SaveChanges();

            var Boeing737Model = new AircraftModel { AircraftModelId = Guid.NewGuid(), AircraftManufacturer = Boeing, Name = "Boeing 737", Seats = 170, CruiseSpeed = 514 };
            var Boeing777Model = new AircraftModel { AircraftModelId = Guid.NewGuid(), AircraftManufacturer = Boeing, Name = "Boeing 777", Seats = 550, CruiseSpeed = 560 };
            var AirbusA380Model = new AircraftModel { AircraftModelId = Guid.NewGuid(), AircraftManufacturer = Airbus, Name = "Airbus A380", Seats = 850, CruiseSpeed = 560 };
            var AirbusA340Model = new AircraftModel { AircraftModelId = Guid.NewGuid(), AircraftManufacturer = Airbus, Name = "Airbus A340", Seats = 475, CruiseSpeed = 490 };
            var L1011Model = new AircraftModel { AircraftModelId = Guid.NewGuid(), AircraftManufacturer = LockheedMartin, Name = "L-1011", Seats = 400, CruiseSpeed = 540 };
            var Embraer175Model = new AircraftModel { AircraftModelId = Guid.NewGuid(), AircraftManufacturer = Embraer, Name = "Embraer 175", Seats = 80, CruiseSpeed = 450 };
            if (!context.AircraftModel.Any())
            {
                context.AircraftModel.AddRange(Boeing737Model, Boeing777Model, AirbusA340Model, AirbusA380Model, L1011Model, Embraer175Model);
            }
            context.SaveChanges();


            var Boeing737 = new Aircraft
            {
                AircraftId = Guid.NewGuid(),
                AircraftModel = Boeing737Model,
                ProductionDate = DateTime.Now,
                LastInspection = DateTime.Now
            };
            var Boeing777 = new Aircraft
            {
                AircraftId = Guid.NewGuid(),
                AircraftModel = Boeing777Model,
                ProductionDate = DateTime.Now,
                LastInspection = DateTime.Now
            };

            var AirbusA380 = new Aircraft
            {
                AircraftId = Guid.NewGuid(),
                AircraftModel = AirbusA380Model,
                ProductionDate = DateTime.Now,
                LastInspection = DateTime.Now
            };
            var AirbusA340 = new Aircraft
            {
                AircraftId = Guid.NewGuid(),
                AircraftModel = AirbusA340Model,
                ProductionDate = DateTime.Now,
                LastInspection = DateTime.Now
            };
            var L1011 = new Aircraft
            {
                AircraftId = Guid.NewGuid(),
                AircraftModel = L1011Model,
                ProductionDate = DateTime.Now,
                LastInspection = DateTime.Now
            };
            var Embraer175 = new Aircraft
            {
                AircraftId = Guid.NewGuid(),
                AircraftModel = Embraer175Model,
                ProductionDate = DateTime.Now,
                LastInspection = DateTime.Now
            };
            if(!context.Aircraft.Any())
            {
                context.Aircraft.AddRange(Boeing737,Boeing777,AirbusA340,AirbusA380,Embraer175,L1011);  // DB has been seeded
            }
            context.SaveChanges();

            var flightCalculator = serviceProvider.GetService<FlightCalculatorService>();
            var ChopinAirport = new Airport { Id = Guid.NewGuid(), Name = "Chopin Airport", Latitude = 52.1657, Longitude = 20.9671 };
            var SingaporeAirport = new Airport { Id = Guid.NewGuid(), Name = "Singapore Changi Airport", Latitude = 1.3644, Longitude = 103.9915 };
            var AtlantaAirport = new Airport { Id = Guid.NewGuid(), Name = "Hartsfield-Jackson Atlanta International Airport", Latitude = 33.6407, Longitude = -84.4277 };
            var CairoAirport = new Airport { Id = Guid.NewGuid(), Name = "Cairo International Airport", Latitude = 30.1117, Longitude = 31.4139 };

            if (!context.Airport.Any())
            {
                context.Airport.AddRange(ChopinAirport, SingaporeAirport, SingaporeAirport, AtlantaAirport, CairoAirport);
            }
            context.SaveChanges();

            var WarsawToSingapore = new Flight { FlightId = Guid.NewGuid(),
                Aircraft = AirbusA340,
                DepartureAirport = ChopinAirport,
                ArrivalAirport = SingaporeAirport,
                DepartureTime = DateTime.Today.AddDays(1), 
                EstimatedArrivalTime = DateTime.Today.AddDays(1).AddMinutes(flightCalculator.CalculateTravelTime(AirbusA340, ChopinAirport, SingaporeAirport))
                };

            var AtlantaToCairo = new Flight
            {
                FlightId = Guid.NewGuid(),
                Aircraft = AirbusA380,
                DepartureAirport = AtlantaAirport,
                ArrivalAirport = CairoAirport,
                DepartureTime = DateTime.Today.AddDays(2),
                EstimatedArrivalTime = DateTime.Today.AddDays(1).AddMinutes(flightCalculator.CalculateTravelTime(AirbusA340, AtlantaAirport, CairoAirport))
                };
            if (!context.Flight.Any())
            {
                context.Flight.AddRange(WarsawToSingapore, AtlantaToCairo);
            }

            context.SaveChanges();

        }
    }
}