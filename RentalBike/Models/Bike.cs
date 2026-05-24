using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RentalBike.Models
{
    public class Bike
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Model { get; set; }
        [Precision(6, 2), Required]
        public decimal Price { get; set; }
        public string Color { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = false;
        public int Quantity { get; set; } = 0;

    }
}