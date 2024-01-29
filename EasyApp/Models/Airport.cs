using System.ComponentModel.DataAnnotations;

namespace Airtrafic.Models
{
    public class Airport
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }

        [Range(-90,90)]
        public double Latitude { get; set; }

        [Range(-180,180)]
        public double Longitude { get; set; }

        ICollection<Flight> Flights { get; set; } = new List<Flight>();
    }
}
