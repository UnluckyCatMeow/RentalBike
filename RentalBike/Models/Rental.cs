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


        public Bike Bike { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
