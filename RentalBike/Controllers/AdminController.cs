using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalBike.Data;
using RentalBike.Models;

namespace RentalBike.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Bikes()
        {
            var bikes = await _context.Bikes.ToListAsync();
            return View(bikes);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bike bike)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bike);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Bikes));
            }
            return View(bike);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var bike = await _context.Bikes.FindAsync(id);
            if (bike == null) return NotFound();
            return View(bike);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bike bike)
        {
            if (id != bike.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bike);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Bikes.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Bikes));
            }
            return View(bike);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var bike = await _context.Bikes.FindAsync(id);
            if (bike == null) return NotFound();
            return View(bike);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bike = await _context.Bikes.FindAsync(id);
            if (bike != null) _context.Bikes.Remove(bike);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Bikes));
        }
        public async Task<IActionResult> Rentals()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Bike)
                .Include(r => r.User)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync();
            return View(rentals);
        }
        [HttpPost]
        public async Task<IActionResult> CompleteRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                rental.Status = "Completed";
                rental.EndDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Rentals));
        }
        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
    }
}