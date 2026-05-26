using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalBike.Data;
using RentalBike.Models;
using System.Security.Claims;

namespace RentalBike.Controllers
{
    public class RentalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Форма оренди
        [Authorize]
        public async Task<IActionResult> Rent(int id)
        {
            var bike = await _context.Bikes.FindAsync(id);
            if (bike == null) return NotFound();
            ViewBag.Bike = bike;
            return View();
        }

        // POST: Створення оренди
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Rent(int id, int hours, string fullName, string phone, string email)
        {
            var bike = await _context.Bikes.FindAsync(id);
            if (bike == null) return NotFound();

            if (hours < 1 || hours > 24)
            {
                ModelState.AddModelError("", "Години мають бути від 1 до 24");
                ViewBag.Bike = bike;
                return View();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rental = new Rental
            {
                BikeId = id,
                UserId = userId,
                Hours = hours,
                TotalPrice = bike.Price * hours,
                FullName = fullName,
                Phone = phone,
                Email = email,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(hours),
                Status = "Pending"
            };

            _context.Rentals.Add(rental);
            bike.Quantity--;
            if (bike.Quantity <= 0) bike.IsAvailable = false;
            await _context.SaveChangesAsync();

            return RedirectToAction("MyRentals");
        }

        // Мої оренди
        [Authorize]
        public async Task<IActionResult> MyRentals()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rentals = await _context.Rentals
                .Include(r => r.Bike)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync();
            return View(rentals);
        }
    }
}