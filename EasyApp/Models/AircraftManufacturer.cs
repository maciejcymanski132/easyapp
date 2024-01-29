using System.ComponentModel.DataAnnotations;

namespace Airtrafic.Models
{
    public class AircraftManufacturer
    {
        [Required]
        public Guid AircraftManufacturerId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<AircraftModel> AircraftModels { get; set; }
    }
}
