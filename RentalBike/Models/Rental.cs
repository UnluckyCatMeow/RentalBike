using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RentalBike.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public int BikeId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Precision(6, 2)]
        public decimal TotalPrice { get; set; }
        public int Hours { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public Bike? Bike { get; set; }
        public ApplicationUser? User { get; set; }
    }
}