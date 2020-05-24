using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.ViewModels
{
    public class TransactionViewModel
    {
        [Required(ErrorMessage = "Transaction account must be specified")]        
        public int FromAccountId { get; set; }
        public decimal OldAccountBalance { get; set; }

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
        [Range(0, int.MaxValue)]        
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "The balance cannot go under 0.")]
        [Range(0, int.MaxValue, ErrorMessage = "The amount cannot be negative.")]
        public decimal Balance { get; set; }
        [MaxLength(50)]
        public string Symbol { get; set; }
        [MaxLength(50)]
        public string Bank { get; set; }
        [MaxLength(50)]
        public string ToAccount { get; set; }
        
        public IQueryable<SelectListItem> Types { get; set; }
        public IQueryable<SelectListItem> Operations { get; set; }
        public IQueryable<SelectListItem> Symbols { get; set; }
        public ErrorMessageViewModel ErrorMessageViewModel { get; set; }
    }    
}
