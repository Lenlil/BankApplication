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
    public class AccountController : Controller
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

        public AccountController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IAccountsRepository accountRepository,
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

        public IActionResult ShowAccount(int id)
        {
            bool ok = true;

            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong.");

                return View();
            }            

            var account = _accountsRepository.GetOneByID(id);
            //var transactionsOnAccount = _accountServices.GetTransactionsOnAccount(id);

            var viewModel = _viewmodelsServices.CreateAccountsShowAccountDetailsViewModel(account);


            viewModel.Transactions = _accountServices.GetFrom(id,0).Select(TransactionToTransactionViewModel).ToList();
            return View(viewModel);
        }

        public IActionResult GetFrom(int accountId, int startPos)
        {
            return Json(_accountServices.GetFrom(accountId, startPos).Select(TransactionToTransactionViewModel).ToList());
        }
        private ShowAccountDetailsViewModel.TransactionListViewModel TransactionToTransactionViewModel(Transactions x)
        {
            return new ShowAccountDetailsViewModel.TransactionListViewModel
            {
                TransactionId = x.TransactionId,
                Date = x.Date.ToShortDateString(),
                Type = x.Type,
                Operation = x.Operation,
                Amount = x.Amount,
                Balance = x.Balance,
                Symbol = x.Symbol,
                Bank = x.Bank,
                Account = x.Account,
            };
        }
    }
}