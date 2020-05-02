using Microsoft.AspNetCore.Identity;
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
            SeedData(context);
        }
        private void SeedData(ApplicationDbContext context)
        {            
          
            //context.SaveChanges();
        }
        private void AddRoleIfNotExists(ApplicationDbContext context, string role)
        {
            
        }

        private void AddIfNotExists (UserManager<IdentityUser> userManager, string user, string role)
        {

        }
    }
}
