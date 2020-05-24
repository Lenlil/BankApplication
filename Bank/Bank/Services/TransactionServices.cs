using Bank.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Services
{
    public class TransactionServices
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IDispositionsRepository _dispositionsRepository;
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionServices(IAccountsRepository accountsRepository, IDispositionsRepository dispositionsRepository, ITransactionsRepository transactionsRepository)
        {
            _accountsRepository = accountsRepository;
            _dispositionsRepository = dispositionsRepository;
            _transactionsRepository = transactionsRepository;

        }

        public bool CheckIfToAccountIdIsOk(int toAccountId, int fromAccountId)
        {

        }
    }
}
