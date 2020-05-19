using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class CreateTransactionViewModel
    {
        [Required(ErrorMessage = "Transaction account must be specified")]        
        public int FromAccountId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Transaction date must be specified")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Type must be specified")]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required(ErrorMessage = "Operation must be specified")]
        [MaxLength(50)]
        public string Operation { get; set; }

        [Required(ErrorMessage = "Amount must be specified")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Balance must be specified")]
        public decimal Balance { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string ToAccount { get; set; }

        public IQueryable<SelectListItem> Operations { get; set; }
        public IQueryable<SelectListItem> Types { get; set; }
        public IQueryable<SelectListItem> Symbols { get; set; }
    }
}
