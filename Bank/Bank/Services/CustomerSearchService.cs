using Bank.Interfaces;
using Bank.Models;
using Bank.ViewModels;
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

        public IQueryable<Customers> GetCustomersMatchingSearch(string search)
        {
            var customersToSearch = _customersRepository.GetAll();

            IQueryable<Customers> searchResultCustomers = customersToSearch
                                      .Where(s => s.City.Contains(search) ||
                                                s.Givenname.Contains(search) ||
                                                s.Surname.Contains(search));                                 
            return searchResultCustomers;
        }  

        public bool CheckIfThereAreSearchValuesNameCity(SearchViewModel model)
        {
            var name = model.CustomerNameSearch;
            var city = model.CustomerCitySearch;           

            if (name == null && city == null)
            {
                return false;
            }

            return true;
        }                
    }
}
