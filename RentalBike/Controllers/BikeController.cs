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

        public async Task<IActionResult> Index(string search, string type, string sort)
        {
            var bikes = _context.Bikes.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                bikes = bikes.Where(b => b.Model.Contains(search));

            if (!string.IsNullOrEmpty(type) && type != "all")
                bikes = bikes.Where(b => b.Type == type);

            switch (sort)
            {
                case "price_asc": bikes = bikes.OrderBy(b => b.Price); break;
                case "price_desc": bikes = bikes.OrderByDescending(b => b.Price); break;
                default: bikes = bikes.OrderBy(b => b.Model); break;
            }

            ViewBag.Types = await _context.Bikes.Select(b => b.Type).Distinct().ToListAsync();
            ViewBag.CurrentType = type;
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sort;

            return View(await bikes.ToListAsync());
        }
    }
}