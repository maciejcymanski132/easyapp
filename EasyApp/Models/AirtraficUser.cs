using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Airtrafic.Models
{
    public class AirtraficUser:IdentityUser
    {
        [Display(Name = "Role")]
        public AirtraficUserRole? UserRole { get; set; }
        public string? UserRoleId { get; set; }
    }
}
