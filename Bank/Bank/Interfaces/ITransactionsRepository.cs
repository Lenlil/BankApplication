using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Interfaces
{
    interface ITransactionsRepository
    {
        IQueryable<Transactions> GetAll();
        Transactions GetOneByID(int transactionId);
        void Create(Transactions transaction);
        void Update(Transactions transaction);
    }
}
