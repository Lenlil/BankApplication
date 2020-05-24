using Bank.Interfaces;
using Bank.ViewModels;
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
        private readonly ViewModelsService _viewmodelsServices;

        public TransactionServices(IAccountsRepository accountsRepository, IDispositionsRepository dispositionsRepository, ITransactionsRepository transactionsRepository, ViewModelsService viewmodelsServices)
        {
            _accountsRepository = accountsRepository;
            _dispositionsRepository = dispositionsRepository;
            _transactionsRepository = transactionsRepository;
            _viewmodelsServices = viewmodelsServices;

        }

        public TransferThisBankTransactionViewModel CheckModelIsOkAndReturnViewmodel(TransferThisBankTransactionViewModel model)
        {
            var viewModel = _viewmodelsServices.CreateTransferThisBankTransactionViewModel(model.FromAccountId);

            if (!IsToAccountOk(model.ToAccountId, model.FromAccountId))
            {               
                viewModel.ErrorMessageViewModel.ErrorMessage = "Enter the Account ID you want to transfer to.";

                return viewModel;
            }

            if (!IsAmountOk(model.Amount))
            {                
                viewModel.ErrorMessageViewModel.ErrorMessage = "The amount entered cannot be negative or 0.";

                return viewModel;
            }

            if (!IsDateOk(model.Date))
            {                
                viewModel.ErrorMessageViewModel.ErrorMessage = "You cannot make a transaction in the past.";

                return viewModel;
            }          

            if (!DoesToAccountExistInThisBank(model.ToAccountId))
            {                
                viewModel.ErrorMessageViewModel.ErrorMessage = "No such account esixts. Enter the Account ID you want to transfer to.";

                return viewModel;
            }                       

            if (!IsBalanceEnough(model.Amount, model.OldAccountBalance))
            {               
                viewModel.ErrorMessageViewModel.ErrorMessage = "Insufficient funds on account to perform the transaction.";

                return viewModel;
            }

            return viewModel;
        }

        private bool IsToAccountOk(int toAccountId, int fromAccountId)
        {
            if (toAccountId <= 0 || toAccountId == fromAccountId)
            {
                return false;
            }
            return true;
        }

        private bool IsAmountOk(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }
            return true;
        }

        private bool IsDateOk(DateTime date)
        {
            if ((date < DateTime.Now) && (date.Date != DateTime.Now.Date))
            {
                return false;
            }
            return true;
        }

        private bool DoesToAccountExistInThisBank(int toAccountId)
        {
            var targetAccount = _accountsRepository.GetOneByID(toAccountId);

            if (targetAccount == null)
            {
                return false;
            }
            return true;
        }
        private bool IsBalanceEnough(decimal amount, decimal balance)
        {           
            if (amount > balance)
            {
                return false;
            }
            return true;
        }
    }
}
