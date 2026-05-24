using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalBike.Data;
using RentalBike.Models;

namespace RentalBike.Controllers
{
    public class BikeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BikeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bikes = await _context.Bikes.ToListAsync();
            return View(bikes);
        }
    }
}