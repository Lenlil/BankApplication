using Bank.Interfaces;
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

        public AccountServices(IAccountsRepository repository)
        {
            _accountsRepository = repository;

        }
        public decimal GetBalanceOnAccount(Accounts account)
        {
            var accountToCheck = _accountsRepository.GetOneByID(account.AccountId);
            decimal balance = accountToCheck.Balance;
            return balance;
        }
    }
}
