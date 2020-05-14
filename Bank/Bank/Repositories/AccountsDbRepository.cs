using Bank.Data;
using Bank.Interfaces;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Repositories
{
    public class AccountsDbRepository : IAccountsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AccountsDbRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Accounts account)
        {
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
        }

        public decimal GetBalanceOnAccount(Accounts account)
        {
            var accountToCheck = GetOneByID(account.AccountId);
            decimal balance = accountToCheck.Balance;
            return balance;
        }

        public IQueryable<Accounts> GetAll()
        {
            return dbContext.Accounts;
        }

        public Accounts GetOneByID(int accountId)
        {
            return dbContext.Accounts.Find(accountId);
        }

        public void Update(Accounts account)
        {
            dbContext.Accounts.Update(account);
            dbContext.SaveChanges();
        }
    }
}
