using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Interfaces
{
    public interface IDispositionsRepository
    {
        IQueryable<Dispositions> GetAll();
        Dispositions GetOneByID(int customerId);
        void Create(Dispositions disposition);
        void Update(Dispositions disposition);
    }
}
