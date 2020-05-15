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

        public IQueryable<Customers> GetCustomersMatchingSearch(string searchName, string searchCity)
        {
            var allCustomers = _customersRepository.GetAll();

            if (searchName.Contains(" "))
            {            
                string searchFirstname = GetFirstnameString(searchName);
                string searchSurname = GetSurnameString(searchName);

                IQueryable<Customers> searchResultCustomers = SearchCustomers(allCustomers, searchFirstname, searchSurname, searchCity);

                return searchResultCustomers;
            }
            else
            {
                IQueryable<Customers> searchResultCustomers = SearchCustomers(allCustomers, searchName, searchCity);

                return searchResultCustomers;
            }                                                       
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

        private IQueryable<Customers> SearchCustomers(IQueryable<Customers> customersToSearch, string firstname, string surname, string city)
        {
            IQueryable<Customers> searchResultCustomers = customersToSearch
                                   .Where(x => x.Givenname.Contains(firstname) && x.Surname.Contains(surname) || x.City.Contains(city))
                                   .Select(x =>
                                  new Customers()
                                  {
                                      CustomerId = x.CustomerId,
                                      NationalId = x.NationalId,
                                      Givenname = x.Givenname,
                                      Surname = x.Surname,
                                      Streetaddress = x.Streetaddress,
                                      City = x.City
                                  });
            return searchResultCustomers;
        }

        private IQueryable<Customers> SearchCustomers(IQueryable<Customers> customersToSearch, string firstname, string city)
        {
            IQueryable<Customers> searchResultCustomers = customersToSearch
                                   .Where(x => x.Givenname.Contains(firstname) || x.Surname.Contains(firstname) || x.City.Contains(city))
                                   .Select(x =>
                                  new Customers()
                                  {
                                      CustomerId = x.CustomerId,
                                      NationalId = x.NationalId,
                                      Givenname = x.Givenname,
                                      Surname = x.Surname,
                                      Streetaddress = x.Streetaddress,
                                      City = x.City
                                  });
            return searchResultCustomers;
        }

        private string GetFirstnameString(string name)
        {
            string[] splitString = name.Split(new char[0]);
            string firstname = splitString[0];
            return firstname;
        }

        private string GetSurnameString(string name)
        {
            string[] splitString = name.Split(new char[0]);
            string firstname = splitString[1];
            return firstname;
        }
    }
}
