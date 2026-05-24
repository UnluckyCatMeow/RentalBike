using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RentalBike.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public bool IsBlocked { get; set; } = false;
    }
}
