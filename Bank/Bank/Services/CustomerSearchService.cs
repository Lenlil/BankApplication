using Bank.Interfaces;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services
{    
    public class CustomerSearchService
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomerSearchService(ICustomersRepository repository)
        {
            _customersRepository = repository;

        }

        public IEnumerable<Customers> GetCustomersMatchingSearch(string customerName, string city)
        {
            var allCustomers = _customersRepository.GetAll();

            return allCustomers;
        }
       
    }
}
