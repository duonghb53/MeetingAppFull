using MeetingAppCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingAppCore.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var roles = new List<AppRole>
            {
                new AppRole{Id = Guid.NewGuid(), Name = "Admin"},
                new AppRole{Id = Guid.NewGuid(), Name = "Owner"},
                new AppRole{Id = Guid.NewGuid(), Name = "Guest"}
            };

            if (!await roleManager.Roles.AnyAsync())
            {
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            var users = new List<AppUser> {
                new AppUser { UserName = "duonghb53", DisplayName = "Duong Ha" },
                new AppUser{ UserName="ubuntu", DisplayName = "Ubuntu" },
                new AppUser{UserName="daisy", DisplayName = "Daisy" }
            };

            foreach (var user in users)
            {
                //user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "admin@123");
                await userManager.AddToRoleAsync(user, "Guest");
            }

            var admin = new AppUser { UserName = "admin", DisplayName = "Administrator" };
            await userManager.CreateAsync(admin, "admin@123");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Owner", "Guest" });
        }
    }
}
