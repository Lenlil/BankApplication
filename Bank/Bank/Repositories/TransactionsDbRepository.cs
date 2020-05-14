using Bank.Data;
using Bank.Interfaces;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Repositories
{
    public class TransactionsDbRepository : ITransactionsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TransactionsDbRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Transactions transaction)
        {
            dbContext.Transactions.Add(transaction);
            dbContext.SaveChanges()
        }

        public IQueryable<Transactions> GetAll()
        {
            return dbContext.Transactions;
        }

        public Transactions GetOneByID(int transactionId)
        {
            return dbContext.Transactions.Find(transactionId);
        }

        public void Update(Transactions transaction)
        {
            dbContext.Transactions.Update(transaction);
            dbContext.SaveChanges();
        }
    }
}
