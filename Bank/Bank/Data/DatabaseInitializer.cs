using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Data
{
    public class DatabaseInitializer
    {
        public void Initialize(ApplicationDbContext context)
        {
            //context.Database.EnsureCreated();
            context.Database.Migrate();
            SeedData(context);
        }
        private void SeedData(ApplicationDbContext context)
        {
            //Om vi vill lägga till testdata gör vi det här genom SeedData..
          
            //context.SaveChanges();
        }
    }
}
