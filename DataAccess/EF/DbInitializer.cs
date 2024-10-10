using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EF.Models;

namespace DataAccess.EF
{
    public class DbInitializer
    {
        public static async Task Initialize(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Crear la base de datos si no existe
            context.Database.EnsureCreated();

            // Crear roles si no existen
            string[] roleNames = { "Admin", "User", "Manager" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Crear un usuario de ejemplo y asignar rol
            var adminUser = new IdentityUser { UserName = "sa", Email = "admin@domain.com" };
            string adminPassword = "Password123!";
            var user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
