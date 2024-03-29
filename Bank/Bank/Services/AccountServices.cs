﻿using Bank.Interfaces;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services
{
    public class AccountServices
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IDispositionsRepository _dispositionsRepository;
        private readonly ITransactionsRepository _transactionsRepository;

        public AccountServices(IAccountsRepository accountsRepository, IDispositionsRepository dispositionsRepository, ITransactionsRepository transactionsRepository)
        {
            _accountsRepository = accountsRepository;
            _dispositionsRepository = dispositionsRepository;
            _transactionsRepository = transactionsRepository;

        }
        public decimal GetBalanceOnAccount(Accounts account)
        {
            var accountToCheck = _accountsRepository.GetOneByID(account.AccountId);
            decimal balance = accountToCheck.Balance;
            return balance;
        }

        public IQueryable<Accounts> GetAccountsOfCustomer(int customerId)
        {
            var customerDispositions = GetCustomerDispositionsList(customerId);
            return GetCustomerAccountsList(customerDispositions);
        }

        public decimal GetBalanceOnAllCustomerAccounts(int customerId)
        {           
            var customerAccounts = GetAccountsOfCustomer(customerId);

            decimal totalAccountsBalance = 0;

            foreach (var account in customerAccounts)
            {
                totalAccountsBalance += account.Balance;
            }

            return totalAccountsBalance;
        }      

        private IQueryable<Dispositions> GetCustomerDispositionsList(int customerId)
        {            
            return _dispositionsRepository.GetAll().Where(r => r.CustomerId == customerId);
        }

        private IQueryable<Accounts> GetCustomerAccountsList(IQueryable<Dispositions> customerDispositions)
        {
            var accountId = customerDispositions.Select(x => x.AccountId);
            var allAccounts = _accountsRepository.GetAll();
            IQueryable<Accounts> customerAccountsList;

            customerAccountsList = allAccounts
                                    .Where(x => x.AccountId == accountId.FirstOrDefault())
                                    .Select(x =>
                                   new Accounts()
                                   {
                                       AccountId = x.AccountId,
                                       Frequency = x.Frequency,
                                       Created = x.Created,
                                       Balance = x.Balance
                                   });

            return customerAccountsList;
        }

        public IQueryable<Transactions> GetTransactionsOnAccount(int accountId)
        {
            return _transactionsRepository.GetAll().Where(r => r.AccountId == accountId);
        }

        public IQueryable<Transactions> GetFrom(int accountId, int startPos = 0)
        {
            return GetTransactionsOnAccount(accountId).OrderByDescending(x => x.TransactionId).Skip(startPos).Take(20);
        }

    }
}
