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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShowCustomerSearchResults(SearchViewModel searchModel)
        {
            bool ok = true;

            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong.");
                return View("../Home/Search", searchModel);
            }

            var isAFieldFilledIn = _customerSearchService.CheckIfThereAreSearchValuesNameCity(searchModel);
                       
            if (isAFieldFilledIn)
            {
                var name = searchModel.CustomerNameSearch;
                var city = searchModel.CustomerCitySearch;
                var resultCustomers = _customerSearchService.GetCustomersMatchingSearch(name, city);

                var model = new SearchResultsViewModel();
                _viewmodelsServices.CreateSearchResultsViewModel(model, resultCustomers);

                return View(model);
            }
            else
            {
                var resultCustomers = _customersRepository.GetAll();
                var model = new SearchResultsViewModel();
                _viewmodelsServices.CreateSearchResultsViewModel(model, resultCustomers);

                return View(model);
            }                                                    
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShowCustomerBySearchId(SearchViewModel searchModel)
        {
            bool ok = true;

            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Please fill in the required fields.");

                return View("../Home/Search", searchModel);
            }           

            var customer = _customersRepository.GetOneByID(searchModel.CustomerIdSearch);
            var customerAccounts = _accountServices.GetAccountsOfCustomer(customer.CustomerId);

            var model = new ShowCustomerDetailsViewModel();

            _viewmodelsServices.CreateCustomerViewModelForShowDetails(model, customer);           
            _viewmodelsServices.CreateAccountViewModelForShowDetails(model, customerAccounts);      
            model.TotalAmountOnAccounts = _accountServices.GetBalanceOnAllCustomerAccounts(customer.CustomerId);

            return View("ShowCustomer", model);
        }      
               
        public IActionResult ShowCustomer(int customerId)
        {
            bool ok = true;

            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Please fill in the required fields.");

                return View("ShowCustomer");
            }

            var customer = _customersRepository.GetOneByID(customerId);
            var customerAccounts = _accountServices.GetAccountsOfCustomer(customer.CustomerId);

            var model = new ShowCustomerDetailsViewModel();

            _viewmodelsServices.CreateCustomerViewModelForShowDetails(model, customer);
            _viewmodelsServices.CreateAccountViewModelForShowDetails(model, customerAccounts);
            model.TotalAmountOnAccounts = _accountServices.GetBalanceOnAllCustomerAccounts(customer.CustomerId);

            return View(model);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}