using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Interfaces
{
    public interface IAccountsRepository
    {
        IQueryable<Accounts> GetAll();
        Accounts GetOneByID(int accountId);
        void Create(Accounts account);
        void Update(Accounts account);
        decimal GetBalanceOnAccount(Accounts account);   
    }
}
