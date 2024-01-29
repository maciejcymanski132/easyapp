using System.ComponentModel.DataAnnotations;

namespace Airtrafic.Models
{
    public class Ticket
    {
        [Required]
        public Guid TicketId { get; set; }
       
        [Required]
        public Guid FlightId { get; set; }

        [Required]
        public Flight Flight { get; set; }
    }
}
