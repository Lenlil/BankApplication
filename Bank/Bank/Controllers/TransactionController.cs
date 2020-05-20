using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Data;
using Bank.Interfaces;
using Bank.Models;
using Bank.Services;
using Bank.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bank.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;
        private readonly IAccountsRepository _accountsRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly IDispositionsRepository _dispositionsRepository;
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly CustomerSearchService _customerSearchService;
        private readonly AccountServices _accountServices;
        private readonly ViewModelsService _viewmodelsServices;

        public TransactionController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IAccountsRepository accountRepository,
            ICustomersRepository customersRepository, IDispositionsRepository dispositionsRepository, ITransactionsRepository transactionsRepository, CustomerSearchService searchService, AccountServices accountServices, ViewModelsService viewmodelsServices)
        {
            _logger = logger;
            this.dbContext = dbContext;
            _accountsRepository = accountRepository;
            _customersRepository = customersRepository;
            _dispositionsRepository = dispositionsRepository;
            _transactionsRepository = transactionsRepository;
            _customerSearchService = searchService;
            _accountServices = accountServices;
            _viewmodelsServices = viewmodelsServices;
        }

        public IActionResult Withdrawal(int id)
        {
            var model = _viewmodelsServices.CreateWithdrawalViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Withdrawal(AddTransactionViewModel model)
        {
            bool ok = true;

            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Please fill in all the required fields.");

                var viewModel = _viewmodelsServices.CreateDepositViewModel(model.FromAccountId);

                return View(viewModel);
            }
            if (model.Amount <= 0)
            {
                var viewModel = _viewmodelsServices.CreateDepositViewModel(model.FromAccountId);

                viewModel.ErrorMessageViewModel.ErrorMessage = "The amount entered cannot be negative or 0.";

                return View(viewModel);
            }
            if ((model.Date < DateTime.Now) && (model.Date.Date != DateTime.Now.Date))
            {
                var viewModel = _viewmodelsServices.CreateDepositViewModel(model.FromAccountId);

                viewModel.ErrorMessageViewModel.ErrorMessage = "You cannot make a transaction in the past.";

                return View(viewModel);
            }

            var account = _accountsRepository.GetOneByID(model.FromAccountId);
            var oldBalance = model.OldAccountBalance;

            if (model.Amount > oldBalance)
            {
                var viewModel = _viewmodelsServices.CreateAddTransactionViewModel(model.FromAccountId);

                viewModel.ErrorMessageViewModel.ErrorMessage = "Insufficient funds on account to perform the transaction.";

                return View(viewModel);
            }          
           
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

            return View("SuccessConfirmation");
            
        }
        public IActionResult Deposit(int id)
        {
            var model = _viewmodelsServices.CreateDepositViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deposit(AddTransactionViewModel model)
        {
            bool ok = true;

            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Please fill in all the required fields.");

                var viewModel = _viewmodelsServices.CreateDepositViewModel(model.FromAccountId);

                return View(viewModel);
            }           
            if (model.Amount <= 0)
            {
                var viewModel = _viewmodelsServices.CreateDepositViewModel(model.FromAccountId);

                viewModel.ErrorMessageViewModel.ErrorMessage = "The amount entered cannot be negative or 0.";

                return View(viewModel);
            }
            if ((model.Date < DateTime.Now) && (model.Date.Date != DateTime.Now.Date))
            {
                var viewModel = _viewmodelsServices.CreateDepositViewModel(model.FromAccountId);

                viewModel.ErrorMessageViewModel.ErrorMessage = "You cannot make a transaction in the past.";

                return View(viewModel);
            }

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

            return View("SuccessConfirmation");          
            
        }

        public IActionResult CreateTransaction(int id)
        {
            var model = _viewmodelsServices.CreateAddTransactionViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTransaction(AddTransactionViewModel model)
        {
            bool ok = true;
         
            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Please fill in all the required fields.");

                var viewModel = _viewmodelsServices.CreateAddTransactionViewModel(model.FromAccountId);                         

                return View(viewModel);
            }
            if (model.FromAccountId <= 0)
            {
                var viewModel = _viewmodelsServices.CreateAddTransactionViewModel(model.FromAccountId);              

                viewModel.ErrorMessageViewModel.ErrorMessage = "Enter an Account ID";

                return View(viewModel);
            }
            if (model.Amount <= 0)
            {              
                var viewModel = _viewmodelsServices.CreateAddTransactionViewModel(model.FromAccountId);

                viewModel.ErrorMessageViewModel.ErrorMessage = "The amount entered cannot be negative or 0.";

                return View(viewModel);
            }

            var account = _accountsRepository.GetOneByID(model.FromAccountId);
            var oldBalance = model.OldAccountBalance;          

            if (model.Type == "Debit" && model.Amount > oldBalance)
            {             
                var viewModel = _viewmodelsServices.CreateAddTransactionViewModel(model.FromAccountId);

                viewModel.ErrorMessageViewModel.ErrorMessage = "Insufficient funds on account to perform the transaction. Please change the amount.";

                return View(viewModel);
            }

            //Insättning
            else if (model.Type == "Credit")
            {
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

                return View("SuccessConfirmation");
            }
            //Uttag  
            else
            {
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

                return View("SuccessConfirmation");
            }                                                                             
        }
    }
}