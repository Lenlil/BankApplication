using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Data;
using Bank.Interfaces;
using Bank.Services;
using Bank.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bank.Controllers
{
    public class CustomerController : Controller
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

        public CustomerController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IAccountsRepository accountRepository,
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

        public IActionResult SelectCustomer()
        {
            var model = new SearchViewModel();            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public IActionResult SelectCustomer(SearchViewModel model)
        {
            bool ok = true;

            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Please fill in the required fields.");

                return View(model);
            }

            var customerId = model.CustomerIdSearch;

            var customer = _customersRepository.GetOneByID(customerId);

            if (customer == null)
            {
                ModelState.AddModelError(string.Empty, "The customer doesn't exist.");

                return View(model);
            }

            return RedirectToAction("ShowSelectedCustomer","Customer", new { id = customerId });
        }

        public IActionResult Search(string searchString, string page, string pageSize)
        {
            var customers = _customersRepository.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = _customerSearchService.GetCustomersMatchingSearch(searchString);                
            }                

            var customersIQueryable = _viewmodelsServices.CreateCustomerViewModelsIQueryable(customers);

            var model = _viewmodelsServices.CreateCustomerSearchViewModel(page, pageSize, customersIQueryable);

            return View(model); ;
        }    
         
            public IActionResult ShowSelectedCustomer(int id)
        {
            bool ok = true;

            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong.");

                return View();
            }

            var customer = _customersRepository.GetOneByID(id);
            
            var customerAccounts = _accountServices.GetAccountsOfCustomer(customer.CustomerId);

            var model = new ShowCustomerDetailsViewModel();

            _viewmodelsServices.CreateCustomerViewModelForShowDetails(model, customer);
            _viewmodelsServices.CreateAccountViewModelForShowDetails(model, customerAccounts);
            model.TotalAmountOnAccounts = _accountServices.GetBalanceOnAllCustomerAccounts(customer.CustomerId);

            return View(model);
        }              
    }
}