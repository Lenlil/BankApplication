﻿using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Interfaces
{
    public interface ICustomersRepository
    {
        IEnumerable<Customers> GetList();
        Customers GetOneByID(int customerId);
        void Create(Customers customer);
        void Update(Customers customer);
    }
}
}
