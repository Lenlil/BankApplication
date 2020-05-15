
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class SearchResultsViewModel 
    {
        public IQueryable<SearchResultsCustomer> SearchResultCustomers { get; set; }
        public string ErrorMessage { get; set; }

        public class SearchResultsCustomer
        {
            public int CustomerId { get; set; }
            public string NationalId { get; set; }
            public string CustomerName { get; set; }
            public string CustomerAddress { get; set; }
            public string CustomerCity { get; set; }
        }
    }
}
