using Microsoft.AspNetCore.Identity;

namespace Airtrafic.Models
{
    public class AirtraficUserRole:IdentityRole
    {
        public ICollection<AirtraficUser> Users { get; set; } = new List<AirtraficUser>();

    }
}
