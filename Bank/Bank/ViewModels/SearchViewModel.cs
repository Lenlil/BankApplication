using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class SearchViewModel
    {       
        [Required(ErrorMessage = "Please fill in a Customer ID")]
        public string CustomerIdSearch { get; set; }
        public string CustomerNameSearch { get; set; }        
        public string CustomerCitySearch { get; set; }

    }
}
