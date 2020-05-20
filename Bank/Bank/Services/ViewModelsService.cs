using Bank.Interfaces;
using Bank.Models;
using Bank.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services
{
    public class ViewModelsService
    {        
        private readonly IAccountsRepository _accountsRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly IDispositionsRepository _dispositionsRepository;
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly CustomerSearchService _customerSearchService;
        private readonly AccountServices _accountServices;       

        public ViewModelsService( IAccountsRepository accountRepository,
            ICustomersRepository customersRepository, IDispositionsRepository dispositionsRepository, ITransactionsRepository transactionsRepository, CustomerSearchService searchService, AccountServices accountServices)
        {            
            _accountsRepository = accountRepository;
            _customersRepository = customersRepository;
            _dispositionsRepository = dispositionsRepository;
            _transactionsRepository = transactionsRepository;
            _customerSearchService = searchService;
            _accountServices = accountServices;           
        }

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

        //public ShowAccountDetailsViewModel CreateShowAccountDetailsViewModel(Accounts account, IQueryable<Transactions> customerTransactions)
        //{
        //    var accountToShow = new ShowAccountDetailsViewModel()                            
        //                    {
        //                        AccountId = account.AccountId,
        //                        Frequency = account.Frequency,
        //                        Created = account.Created,
        //                        Balance = account.Balance,
        //                        //NumberVisibleTransactions = 20
        //                        };
        //    var transactions = customerTransactions.Select(x =>
        //                    new ShowAccountDetailsViewModel.TransactionViewModel()
        //                    {
        //                        TransactionId = x.TransactionId,
        //                        Date = x.Date,
        //                        Type = x.Type,
        //                        Operation = x.Operation,
        //                        Amount = x.Amount,
        //                        Balance = x.Balance,
        //                        Symbol = x.Symbol,
        //                        Bank = x.Bank,
        //                        Account = x.Account,
        //                    });

        //   accountToShow.Transactions = transactions.Take(20).OrderByDescending(x => x.Date).ToList();

        //    return accountToShow;
        //}

        public ShowAccountDetailsViewModel CreateAccountsShowAccountDetailsViewModel(Accounts account)
        {
            var accountToShow = new ShowAccountDetailsViewModel()
            {
                AccountId = account.AccountId,
                Frequency = account.Frequency,
                Created = account.Created,
                Balance = account.Balance,                
            };    

            return accountToShow;
        }

        public AddTransactionViewModel CreateAddTransactionViewModel(int accountId)
        {
            var allTransactions = _transactionsRepository.GetAll();
            var account = _accountsRepository.GetOneByID(accountId);
            var oldBalance = _accountServices.GetBalanceOnAccount(account);

            var typesListitems = allTransactions.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.Type.ToString(),
                                      Value = x.Type.ToString()
                                  }).Distinct();

            var operationsListitems = allTransactions.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.Operation.ToString(),
                                      Value = x.Operation.ToString()
                                  }).Distinct();
       
            var model = new AddTransactionViewModel()
            {
                Date = DateTime.Now,
                Types = typesListitems,
                Operations = operationsListitems, 
                FromAccountId = accountId, 
                OldAccountBalance = oldBalance
            };

            model.ErrorMessageViewModel = new ErrorMessageViewModel()
            {
                ErrorMessage = ""       
            };                 

            return model;
        }

        public AddTransactionViewModel AddSelectItemsListToTransactionViewModel(AddTransactionViewModel model)
        {
            var allTransactions = _transactionsRepository.GetAll();

            var typesListitems = allTransactions.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.Type.ToString(),
                                      Value = x.Type.ToString()
                                  }).Distinct();

            var operationsListitems = allTransactions.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.Operation.ToString(),
                                      Value = x.Operation.ToString()
                                  }).Distinct();

            model.Types = typesListitems;
            model.Operations = operationsListitems;

            model.ErrorMessageViewModel = new ErrorMessageViewModel()
            {
                ErrorMessage = ""
            };                     
          
            return model;
        }

        public AddTransactionViewModel CreateDepositViewModel(int accountId)
        {            
            var account = _accountsRepository.GetOneByID(accountId);
            var oldBalance = _accountServices.GetBalanceOnAccount(account);       

            var model = new AddTransactionViewModel()
            {
                Date = DateTime.Now,
                Type = "Credit",
                Operation = "Credit in Cash",
                FromAccountId = accountId,
                OldAccountBalance = oldBalance, 
            };

            model.ErrorMessageViewModel = new ErrorMessageViewModel()
            {
                ErrorMessage = ""
            };

            return model;
        }
    }
}
