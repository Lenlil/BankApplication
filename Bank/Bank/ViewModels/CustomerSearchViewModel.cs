using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class CustomerSearchViewModel 
    {
        public PagingViewModel PagingViewModel { get; set; } = new PagingViewModel();
        public IQueryable<CustomerViewModel> SearchResultCustomers { get; set; }       
        public string SearchString { get; set; }
        public SearchViewModel SearchViewModel { get; set; } = new SearchViewModel();
    }
}
