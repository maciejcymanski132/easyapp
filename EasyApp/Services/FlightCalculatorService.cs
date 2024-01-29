using Airtrafic.Models;
using System;

namespace Airtrafic.Services
{
    public class FlightCalculatorService
    {
        private const double EarthRadiusKm = 6371; // Earth radius in kilometers
        private const double KnotsToKilometersPerHour = 1.852;

        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            lat1 = DegreesToRadians(lat1);
            lon1 = DegreesToRadians(lon1);
            lat2 = DegreesToRadians(lat2);
            lon2 = DegreesToRadians(lon2);

            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = EarthRadiusKm * c;

            return distance;
        }

            private double CalculateTravelTime(double distanceInKilometers, double speedInKnots)
            {
            if (distanceInKilometers < 0 || speedInKnots <= 0)
            {
                throw new ArgumentException("Distance and speed must be non-negative values.");
            }

            // Convert speed from knots to kilometers per hour
            double speedInKilometersPerHour = speedInKnots * KnotsToKilometersPerHour;

            // Calculate time in hours
            double timeInHours = distanceInKilometers / speedInKilometersPerHour;

            // Convert time to minutes
            double travelTimeInMinutes = timeInHours * 60;

            return travelTimeInMinutes;
        }

        public  double CalculateTravelTime(Aircraft aircraft,Airport departureAirport,Airport arrivalAirport)
        {
            var distance = this.CalculateDistance(departureAirport.Latitude, departureAirport.Longitude, arrivalAirport.Latitude, arrivalAirport.Longitude);
            return this.CalculateTravelTime(distance, aircraft.AircraftModel.CruiseSpeed);
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
