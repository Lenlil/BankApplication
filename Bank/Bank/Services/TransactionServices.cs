using Bank.Interfaces;
using Bank.Models;
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
        private readonly AccountServices _accountServices;

        public TransactionServices(IAccountsRepository accountsRepository, IDispositionsRepository dispositionsRepository, ITransactionsRepository transactionsRepository, ViewModelsService viewmodelsServices, AccountServices accountServices)
        {
            _accountsRepository = accountsRepository;
            _dispositionsRepository = dispositionsRepository;
            _transactionsRepository = transactionsRepository;
            _viewmodelsServices = viewmodelsServices;
            _accountServices = accountServices;
        }

        public void CreateTransferThisBankFromAccountTransaction(TransferThisBankTransactionViewModel model)
        {
            var account = _accountsRepository.GetOneByID(model.FromAccountId);
            var oldBalance = model.OldAccountBalance;
            var newBalance = oldBalance - model.Amount;

            var newTransaction = new Transactions()
            {
                AccountId = model.FromAccountId,
                Date = model.Date,
                Type = model.Type,
                Operation = model.Operation,
                Amount = -model.Amount,
                Balance = newBalance,
                Symbol = model.Symbol,
                Bank = model.Bank,
                Account = model.ToAccountId.ToString(),
            };

            _transactionsRepository.Create(newTransaction);

            account.Balance = newBalance;
            _accountsRepository.Update(account);
        }

        public void CreateTransferThisBankToAccountTransaction(TransferThisBankTransactionViewModel model)
        {
            var targetAccount = _accountsRepository.GetOneByID(model.ToAccountId);
            var oldTargetBalance = _accountServices.GetBalanceOnAccount(targetAccount);
            var newTargetBalance = oldTargetBalance + model.Amount;

            var newTargetTransaction = new Transactions()
            {
                AccountId = model.ToAccountId,
                Date = model.Date,
                Type = "Credit",
                Operation = "Collection from Another Account",
                Amount = model.Amount,
                Balance = newTargetBalance,
                Symbol = model.Symbol,
                Bank = model.Bank,
                Account = model.FromAccountId.ToString(),
            };

            _transactionsRepository.Create(newTargetTransaction);

            targetAccount.Balance = newTargetBalance;
            _accountsRepository.Update(targetAccount);
        }

        public void CreateTransferToOtherBankTransaction (TransferTransactionViewModel model)
        {
            var account = _accountsRepository.GetOneByID(model.FromAccountId);
            var oldBalance = model.OldAccountBalance;
            var newBalance = oldBalance - model.Amount;

            var newTransaction = new Transactions()
            {
                AccountId = model.FromAccountId,
                Date = model.Date,
                Type = model.Type,
                Operation = model.Operation,
                Amount = -model.Amount,
                Balance = newBalance,
                Symbol = model.Symbol,
                Bank = model.Bank,
                Account = model.ToAccount,
            };

            _transactionsRepository.Create(newTransaction);

            account.Balance = newBalance;
            _accountsRepository.Update(account);
        }

        public TransferThisBankTransactionViewModel CheckTransferThisBankModelIsOkAndReturnViewmodel(TransferThisBankTransactionViewModel model)
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

        public TransferTransactionViewModel CheckTransferOtherBankModelIsOkAndReturnViewmodel(TransferTransactionViewModel model)
        {
            var viewModel = _viewmodelsServices.CreateTransferViewModel(model.FromAccountId);

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
