using Microsoft.AspNetCore.Identity;
using RentalBike.Models;

namespace RentalBike.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            //ролі
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // адмін
            string adminEmail = "admin@gmail.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "System",
                    EmailConfirmed = true,
                    IsBlocked = false
                };

                var result = await userManager.CreateAsync(admin, "qwerty1234");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            // тестові вело
            if (!context.Bikes.Any())
            {
                context.Bikes.AddRange(
                    new Bike { Type = "Гібридний", Model = "Trek FX 3", Price = 50, Color = "Чорний", IsAvailable = true, Quantity = 2 },
                    new Bike { Type = "Гірський", Model = "Specialized Rockhopper", Price = 70, Color = "Зелений", IsAvailable = true, Quantity = 1 },
                    new Bike { Type = "Міський", Model = "Merida Speeder", Price = 40, Color = "Синій", IsAvailable = true, Quantity = 3 },
                    new Bike { Type = "Гірський", Model = "Cube Reaction", Price = 80, Color = "Червоний", IsAvailable = false, Quantity = 0 },
                    new Bike { Type = "Міський", Model = "Cannondale Quick", Price = 45, Color = "Сірий", IsAvailable = true, Quantity = 2 }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}