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

        public void CreateWithdrawalTransaction (AddTransactionViewModel model)
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

        public void CreateDepositTransaction (AddTransactionViewModel model)
        {
            var account = _accountsRepository.GetOneByID(model.FromAccountId);
            var oldBalance = model.OldAccountBalance;

            var newBalance = oldBalance + model.Amount;

            var newTransaction = new Transactions()
            {
                AccountId = model.FromAccountId,
                Date = model.Date,
                Type = model.Type,
                Operation = model.Operation,
                Amount = model.Amount,
                Balance = newBalance,
                Symbol = model.Symbol,
                Bank = model.Bank,
                Account = model.ToAccount,
            };

            _transactionsRepository.Create(newTransaction);

            account.Balance = newBalance;
            _accountsRepository.Update(account);
        }

        public TransferThisBankTransactionViewModel CheckTransferThisBankModelIsOkAndReturnViewmodel(TransferThisBankTransactionViewModel viewModel)
        {           
            if (!IsToAccountOk(viewModel.ToAccountId, viewModel.FromAccountId))
            {
                viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "Enter the Account ID you want to transfer money to."
                };                

                return viewModel;
            }

            if (!IsAmountOk(viewModel.Amount))
            {
                viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "The amount entered cannot be negative or 0."
                };

                return viewModel;
            }

            if (!IsDateOk(viewModel.Date))
            {
                viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "You cannot make a transaction in the past."
                };

                return viewModel;
            }          

            if (!DoesToAccountExistInThisBank(viewModel.ToAccountId))
            {
                viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "No such account exists. Enter the Account ID you want to transfer to."
                };                

                return viewModel;
            }                       

            if (!IsBalanceEnough(viewModel.Amount, viewModel.OldAccountBalance))
            {
                viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "Insufficient funds on account to perform the transaction."
                };

                return viewModel;
            }

            viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
            {
                ErrorMessage = ""
            };

            return viewModel;
        }

        public TransferTransactionViewModel CheckTransferOtherBankModelIsOkAndReturnViewmodel(TransferTransactionViewModel viewModel)
        {            

            if (!IsAmountOk(viewModel.Amount))
            {
                viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "The amount entered cannot be negative or 0."
                };

                return viewModel;
            }

            if (!IsDateOk(viewModel.Date))
            {
                viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "You cannot make a transaction in the past."
                };

                return viewModel;
            }

            if (!IsBalanceEnough(viewModel.Amount, viewModel.OldAccountBalance))
            {
                viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "Insufficient funds on account to perform the transaction."
                };

                return viewModel;
            }

            viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
            {
                ErrorMessage = ""
            };

            return viewModel;
        }

        public AddTransactionViewModel CheckWithdrawalTransactionModelIsOkAndReturnViewmodel(AddTransactionViewModel viewModel)
        {         

            if (!IsAmountOk(viewModel.Amount))
            {
                viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "The amount entered cannot be negative or 0."
                };                

                return viewModel;
            }

            if (!IsDateOk(viewModel.Date))
            {
                viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "You cannot make a transaction in the past."
                };                

                return viewModel;
            }

            if (!IsBalanceEnough(viewModel.Amount, viewModel.OldAccountBalance))
            {
                viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "Insufficient funds on account to perform the transaction."
                };                

                return viewModel;
            }

            viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
            {
                ErrorMessage = ""
            };

            return viewModel;
        }

        public AddTransactionViewModel CheckDepositTransactionModelIsOkAndReturnViewmodel(AddTransactionViewModel viewModel)
        {           

            if (!IsAmountOk(viewModel.Amount))
            {
                viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "The amount entered cannot be negative or 0."
                };

                return viewModel;
            }

            if (!IsDateOk(viewModel.Date))
            {
                viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "You cannot make a transaction in the past."
                };

                return viewModel;
            }

            viewModel.ErrorMessageViewModel = new ErrorMessageViewModel()
            {
                ErrorMessage = ""
            };

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
