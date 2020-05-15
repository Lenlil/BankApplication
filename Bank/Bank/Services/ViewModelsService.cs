using Bank.Models;
using Bank.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services
{
    public class ViewModelsService
    {
        public ShowCustomerDetailsViewModel CreateCustomerViewModelForShowDetails(ShowCustomerDetailsViewModel model, Customers customer)
        {          
            model.CustomerId = customer.CustomerId;
            model.Gender = customer.Gender;
            model.Givenname = customer.Givenname;
            model.Surname = customer.Surname;
            model.Streetaddress = customer.Streetaddress;
            model.City = customer.City;
            model.Zipcode = customer.Zipcode;
            model.Country = customer.Country;
            model.Birthday = customer.Birthday;
            model.NationalId = customer.NationalId;
            model.Telephonecountrycode = customer.Telephonecountrycode;
            model.Telephonenumber = customer.Telephonenumber;
            model.Emailaddress = customer.Emailaddress;

            return model;
        }

        public ShowCustomerDetailsViewModel CreateAccountViewModelForShowDetails(ShowCustomerDetailsViewModel model, IQueryable<Accounts> customerAccounts)
        {
            model.CustomerAccounts = customerAccounts.Select(x =>
                             new AccountViewModel()
                             {
                                 AccountId = x.AccountId,
                                 Frequency = x.Frequency,
                                 Created = x.Created,
                                 Balance = x.Balance
                             });
            return model;
        }

        public SearchResultsViewModel CreateSearchResultsViewModel(SearchResultsViewModel model, IQueryable<Customers> customers)
        {
            model.SearchResultCustomers = customers.Select(x =>
                                 new SearchResultsViewModel.SearchResultsCustomer()
                                 {
                                     CustomerId = x.CustomerId,
                                     NationalId = x.NationalId,
                                     CustomerName = x.Givenname + " " + x.Surname,
                                     CustomerAddress = x.Streetaddress,
                                     CustomerCity = x.City
                                 });

            return model;
        }
    }
}
