using Airtrafic.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Airtrafic.Models
{
    public class Flight
    {
        [Required]
        public Guid FlightId { get; set; }

        public Guid AircraftId { get; set; }

        [AircraftAirWorthy]
        public Aircraft Aircraft { get; set; }

        public Guid DepartureAirportId { get; set; }

        [Display(Name = "Departure Airport")]
        public Airport DepartureAirport { get; set; }

        public Guid ArrivalAirportId { get; set; }

        [Display(Name = "Arrival Airport")]

        public Airport ArrivalAirport { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Departure")]
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "EAT")]
        public DateTime EstimatedArrivalTime { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
