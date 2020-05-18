using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class ShowAccountDetailsViewModel
    {    
        public IQueryable<Transaction> Transactions { get; set; }
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }

        public class Transaction 
        {
            public int TransactionId { get; set; }         
            public DateTime Date { get; set; }
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
