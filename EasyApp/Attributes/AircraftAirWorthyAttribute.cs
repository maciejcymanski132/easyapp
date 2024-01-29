using Airtrafic.Data;
using Airtrafic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Airtrafic.Attributes
{
    public class AircraftAirWorthyAttribute : ValidationAttribute
    {
   
        public string GetErrorMessage(Aircraft aircraft,DateTime departureTime) =>
            $"Airplane {aircraft.AircraftModel.Name} must be inspected at least a month before the planned flight. Last inspection date: {aircraft.LastInspection}. Planned flight: {departureTime}";

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var flight = validationContext.ObjectInstance as Flight;
            var aircraftContext = validationContext.GetService<AirtraficContext>();
            var aircraft = aircraftContext.Aircraft.Where(a => a.AircraftId == flight.AircraftId).Include(a => a.AircraftModel).FirstOrDefault();
            if (aircraft == null)
            {
                return new ValidationResult("No aircraft with such ID");
            }

            if (aircraft.LastInspection.AddMonths(1) > flight.DepartureTime)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(GetErrorMessage(aircraft, flight.DepartureTime));
            }
        }

    }
}