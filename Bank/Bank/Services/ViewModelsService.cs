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

        public IQueryable<CustomerViewModel> CreateCustomerViewModelsIQueryable(IQueryable<Customers> customers)
        {
            var list = customers.Select(x =>
                            new CustomerViewModel()
                            {
                                CustomerId = x.CustomerId,
                                Gender = x.Gender,
                                Givenname = x.Givenname,
                                Surname = x.Surname,
                                Streetaddress = x.Streetaddress,
                                City = x.City,
                                NationalId = x.NationalId,
                            });

            return list;
        }

        //public SearchResultsViewModel CreateSearchResultsViewModel(SearchResultsViewModel model, IQueryable<Customers> customers)
        //{
        //    if (customers.Count() == 0)
        //    {
        //        model.ErrorMessage = "No Results Found.";
        //    }

        //    model.SearchResultCustomers = customers.Select(x =>
        //                    new SearchResultsViewModel.SearchResultsCustomer()
        //                    {
        //                        CustomerId = x.CustomerId,
        //                        NationalId = x.NationalId,
        //                        CustomerName = x.Givenname + " " + x.Surname,
        //                        CustomerAddress = x.Streetaddress,
        //                        CustomerCity = x.City
        //                    });            

        //    return model;
        //}

        public CustomerSearchViewModel CreateCustomerSearchViewModel(string page, string pageSize, IQueryable<CustomerViewModel> customers)
        {
            var customerSearchViewModel = new CustomerSearchViewModel();
            int currentPage;

            if (string.IsNullOrEmpty(pageSize))
                customerSearchViewModel.PagingViewModel.PageSize = 50;
            else
                customerSearchViewModel.PagingViewModel.PageSize = Convert.ToInt32(pageSize);


            if (string.IsNullOrEmpty(page))
                currentPage = 1;
            else
                currentPage = Convert.ToInt32(page);


            var pageCount = (double)customers.Count() / customerSearchViewModel.PagingViewModel.PageSize;

            customerSearchViewModel.PagingViewModel.MaxPages = (int)Math.Ceiling(pageCount);

            customers = customers.Skip((currentPage - 1) * customerSearchViewModel.PagingViewModel.PageSize).Take(customerSearchViewModel.PagingViewModel.PageSize);

            customerSearchViewModel.PagingViewModel.CurrentPage = currentPage;

            customerSearchViewModel.SearchResultCustomers = customers;

            return customerSearchViewModel;
        }
    }
}
