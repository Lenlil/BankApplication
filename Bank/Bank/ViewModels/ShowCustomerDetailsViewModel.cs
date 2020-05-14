using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class ShowCustomerDetailsViewModel : CustomerViewModel
    {
        public IQueryable<AccountViewModel> CustomerAccounts { get; set; }               

        public decimal TotalAmountOnAccounts { get; set; }       
    }
}
