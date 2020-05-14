using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class SearchViewModel
    {            
        public int CustomerIdSearch { get; set; }
        public string CustomerNameSearch { get; set; }        
        public string CustomerCitySearch { get; set; }

    }
}
