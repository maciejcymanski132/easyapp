using System.ComponentModel.DataAnnotations;

namespace Airtrafic.Models
{
    public class AircraftModel
    {
        [Required]
        public Guid AircraftModelId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(1,1000)]
        public int Seats {  get; set; }

        [Display(Name = "Cruise speed (knots)")]
        [Range(1, 1000)]
        public int CruiseSpeed { get; set; }

        [Required]
        public AircraftManufacturer AircraftManufacturer { get; set; }
    }
}
