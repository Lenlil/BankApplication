﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Data
{
    public class DatabaseInitializer
    {
        public void Initialize(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {           
            context.Database.Migrate();

            AddRoleIfNotExists(context, "Admin");
            AddRoleIfNotExists(context, "Cashier");            
            AddIfNotExists(userManager, "stefan.holmberg@systementor.se", "Admin");
            AddIfNotExists(userManager, "stefan.holmberg@nackademin.se", "Cashier");         
                       
        }
        private void SeedData(ApplicationDbContext context)
        {            
          
            //context.SaveChanges();
        }
        private void AddRoleIfNotExists(ApplicationDbContext context, string role)
        {
            if (context.Roles.Any(r => r.Name == role)) return;
            context.Roles.Add(new IdentityRole { Name = role, NormalizedName = role });
            context.SaveChanges();
        }

        private void AddIfNotExists (UserManager<IdentityUser> userManager, string user, string role)
        {
            if (userManager.FindByEmailAsync(user).Result == null)
            {
                var u = new IdentityUser
                {
                    UserName = user,
                    Email = user,
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(u, "Hejsan123#").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(u, role).Wait();
                }
            }
        }
    }
}
