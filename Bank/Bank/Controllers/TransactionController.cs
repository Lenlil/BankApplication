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
        private readonly TransactionServices _transactionServices;

        public TransactionController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IAccountsRepository accountRepository,
            ICustomersRepository customersRepository, IDispositionsRepository dispositionsRepository, ITransactionsRepository transactionsRepository, CustomerSearchService searchService, AccountServices accountServices, ViewModelsService viewmodelsServices, TransactionServices transactionServices)
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
            _transactionServices = transactionServices;
        }

        public IActionResult TransferThisBank(int id)
        {
            var model = _viewmodelsServices.CreateTransferThisBankTransactionViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TransferThisBank(TransferThisBankTransactionViewModel model)
        {
            bool ok = true;           

            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Please fill in all the required fields.");

                var newModel = _viewmodelsServices.CreateTransferThisBankTransactionViewModel(model.FromAccountId);

                return View(newModel);
            }

            //This validation was originally in CheckTransferThisBankModelIsOkAndReturnViewmodel with the others, but because of unittesting-error I moved it here
            if (!_transactionServices.DoesToAccountExistInThisBank(model.ToAccountId))
            {
                model.ErrorMessageViewModel = new ErrorMessageViewModel()
                {
                    ErrorMessage = "No such account exists. Enter the Account ID you want to transfer to."
                };

                return View(model);
            }

            model = _transactionServices.CheckTransferThisBankModelIsOkAndReturnViewmodel(model);

            if (model.ErrorMessageViewModel.ErrorMessage != "")
            {                         
                return View(model);
            }

            _transactionServices.CreateTransferThisBankFromAccountTransaction(model);
            _transactionServices.CreateTransferThisBankToAccountTransaction(model);
                             
            return View("SuccessConfirmation");                          
        }

        public IActionResult TransferOtherBank(int id)
        {
            var model = _viewmodelsServices.CreateTransferViewModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TransferOtherBank(TransferTransactionViewModel model)
        {
            bool ok = true;

            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Please fill in all the required fields.");

                var newModel = _viewmodelsServices.CreateTransferViewModel(model.FromAccountId);

                return View(newModel);
            }

            model = _transactionServices.CheckTransferOtherBankModelIsOkAndReturnViewmodel(model);

            if (model.ErrorMessageViewModel.ErrorMessage != "")
            {
                return View(model);
            }

            _transactionServices.CreateTransferToOtherBankTransaction(model);                           

            return View("SuccessConfirmation");

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

                var newModel = _viewmodelsServices.CreateWithdrawalViewModel(model.FromAccountId);

                return View(newModel);
            }

            model = _transactionServices.CheckWithdrawalTransactionModelIsOkAndReturnViewmodel(model);

            if (model.ErrorMessageViewModel.ErrorMessage != "")
            {
                return View(model);
            }

            _transactionServices.CreateWithdrawalTransaction(model);            

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

                var newModel = _viewmodelsServices.CreateDepositViewModel(model.FromAccountId);

                return View(newModel);
            }

            model = _transactionServices.CheckDepositTransactionModelIsOkAndReturnViewmodel(model);

            if (model.ErrorMessageViewModel.ErrorMessage != "")
            {
                return View(model);
            }

            _transactionServices.CreateDepositTransaction(model);                     

            return View("SuccessConfirmation");                      
        }

       
    }
}