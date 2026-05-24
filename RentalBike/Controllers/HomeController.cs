using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalBike.Data;
using RentalBike.Models;

namespace RentalBike.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Головна сторінка – показує велосипеди
        public async Task<IActionResult> Index()
        {
            var bikes = await _context.Bikes.ToListAsync();
            return View(bikes);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}