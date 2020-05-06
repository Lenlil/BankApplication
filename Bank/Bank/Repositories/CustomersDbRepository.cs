using Bank.Data;
using Bank.Interfaces;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Repositories
{
    public class CustomersDbRepository : ICustomersRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CustomersDbRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Customers customer)
        {
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
        }

        public IEnumerable<Customers> GetList()
        {
            return dbContext.Customers.ToList();
        }

        public Customers GetOneByID(int customerId)
        {
            return dbContext.Customers.Find(customerId);
        }

        public void Update(Customers customer)
        {
            dbContext.Customers.Update(customer);
            dbContext.SaveChanges();
        }
    }
}
