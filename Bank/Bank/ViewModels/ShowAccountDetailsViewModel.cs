﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class ShowAccountDetailsViewModel
    {    
        public List<TransactionListViewModel> Transactions { get; set; } = new List<TransactionListViewModel>();
        
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
        //public int NumberVisibleTransactions { get; set; }

        public class TransactionListViewModel 
        {
            public int TransactionId { get; set; }         
            public string Date { get; set; }
            public string Type { get; set; }
            public string Operation { get; set; }
            public decimal Amount { get; set; }
            public decimal Balance { get; set; }
            public string Symbol { get; set; }
            public string Bank { get; set; }
            public string Account { get; set; }
        }
    }
}
