
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class SearchResultsViewModel 
    {
        public IQueryable<SearchResultsCustomer> SearchResultCustomers { get; set; }

        public PagingViewModel PagingViewModel { get; set; } = new PagingViewModel();
        public string ErrorMessage { get; set; }

        [Required]
        public string SearchString { get; set; }

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
