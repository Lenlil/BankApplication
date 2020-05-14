using Bank.Data;
using Bank.Interfaces;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Repositories
{
    public class DispositionsDbRepository : IDispositionsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DispositionsDbRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Dispositions disposition)
        {
            dbContext.Dispositions.Add(disposition);
            dbContext.SaveChanges();
        }

        public IQueryable<Dispositions> GetAll()
        {
            return dbContext.Dispositions;
        }

        public Dispositions GetOneByID(int customerId)
        {
            return dbContext.Dispositions.Where(r => r.CustomerId == customerId).FirstOrDefault();
        }

        public void Update(Dispositions disposition)
        {
            dbContext.Dispositions.Update(disposition);
            dbContext.SaveChanges();
        }
    }
}
