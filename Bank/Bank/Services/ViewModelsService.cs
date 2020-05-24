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

        public AddTransactionViewModel CreateWithdrawalViewModel(int accountId)
        {
            var account = _accountsRepository.GetOneByID(accountId);
            var oldBalance = _accountServices.GetBalanceOnAccount(account);

            var model = new AddTransactionViewModel()
            {
                Date = DateTime.Now,
                Type = "Debit",
                Operation = "Withdrawal in Cash",
                FromAccountId = accountId,
                OldAccountBalance = oldBalance,
            };

            model.ErrorMessageViewModel = new ErrorMessageViewModel()
            {
                ErrorMessage = ""
            };

            return model;
        }

        public TransferTransactionViewModel CreateTransferViewModel(int accountId)
        {
            var account = _accountsRepository.GetOneByID(accountId);
            var oldBalance = _accountServices.GetBalanceOnAccount(account);

            var model = new TransferTransactionViewModel()
            {
                Date = DateTime.Now,
                Type = "Debit",
                Operation = "Remittance to Another Bank",
                FromAccountId = accountId,
                OldAccountBalance = oldBalance,
            };

            model.ErrorMessageViewModel = new ErrorMessageViewModel()
            {
                ErrorMessage = ""
            };

            return model;
        }
        public TransferThisBankTransactionViewModel CreateTransferThisBankTransactionViewModel(int accountId)
        {
            var account = _accountsRepository.GetOneByID(accountId);
            var oldBalance = _accountServices.GetBalanceOnAccount(account);

            var model = new TransferThisBankTransactionViewModel()
            {
                Date = DateTime.Now,
                Type = "Debit",
                Operation = "Remittance to Another Account",
                FromAccountId = accountId,
                OldAccountBalance = oldBalance,
                Bank = "Bitcoin Bank"
            };

            model.ErrorMessageViewModel = new ErrorMessageViewModel()
            {
                ErrorMessage = ""
            };

            return model;
        }
    }
}
