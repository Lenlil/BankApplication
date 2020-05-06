﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class StatisticsViewModel
    {
        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public List<Customer> Customers { get; set; }

        public class Account
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Balance { get; set; }
        }

        public List<Account> Accounts { get; set; }       

    }
}