using System.ComponentModel.DataAnnotations;

namespace Airtrafic.Models
{
    public class Aircraft
    {
        [Required]
        public Guid AircraftId { get; set; }

        [Required]
        public AircraftModel AircraftModel { get; set; }

        [Display(Name = "Last Inspection Date")]
        [DataType(DataType.Date)]
        public DateTime LastInspection {  get; set; }

        [Display(Name ="Production Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime ProductionDate { get; set; }

        public ICollection<Flight> Flights { get; set; } = new List<Flight>();
    }
}
