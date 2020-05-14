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

        public CustomerController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IAccountsRepository accountRepository,
            ICustomersRepository customersRepository, IDispositionsRepository dispositionsRepository, ITransactionsRepository transactionsRepository, CustomerSearchService searchService, AccountServices accountServices)
        {
            _logger = logger;
            this.dbContext = dbContext;
            _accountsRepository = accountRepository;
            _customersRepository = customersRepository;            
            _dispositionsRepository = dispositionsRepository;
            _transactionsRepository = transactionsRepository;
            _customerSearchService = searchService;
            _accountServices = accountServices;
        }     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShowCustomerSearchResults(SearchViewModel model)
        {
            bool ok = true;

            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Please fill in the required fields.");

                return View(model);
            }

            var name = model.CustomerNameSearch;
            var city = model.CustomerCitySearch;
            var resultCustomers = _customerSearchService.GetCustomersMatchingSearch(name, city);

            var resultsModel = new SearchResultsViewModel();
            //resultsModel.SearchResultCustomers = resultCustomers;

            return View(resultsModel);

        }

        // GET: Customer/Details/5               

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShowCustomer(SearchViewModel searchModel)
        {
            bool ok = true;

            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Please fill in the required fields.");

                return View();
            }

            var customer = _customersRepository.GetOneByID(searchModel.CustomerIdSearch);

            var model = new ShowCustomerDetailsViewModel(); 
            
            model.CustomerId = customer.CustomerId;
            model.Gender = customer.Gender;
            model.Givenname = customer.Givenname;
            model.Surname =  customer.Surname;
            model.Streetaddress = customer.Streetaddress;
            model.City = customer.City;
            model.Zipcode = customer.Zipcode;
            model.Country = customer.Country;
            model.Birthday = customer.Birthday;
            model.NationalId = customer.NationalId;
            model.Telephonecountrycode = customer.Telephonecountrycode;
            model.Telephonenumber = customer.Telephonenumber;
            model.Emailaddress = customer.Emailaddress;

            var customerAccounts = _accountServices.GetAccountsOfCustomer(customer.CustomerId);
            model.CustomerAccounts = customerAccounts.Select(x =>
                                  new AccountViewModel()
                                  {
                                      AccountId = x.AccountId,
                                      Frequency = x.Frequency,
                                      Created = x.Created,
                                      Balance = x.Balance
                                  });

            model.TotalAmountOnAccounts = _accountServices.GetBalanceOnAllCustomerAccounts(customer.CustomerId);

            return View(model);                      
        }

        //public IActionResult ShowCustomerById(int id)
        //{
        //    var model = new CustomerViewModel();
        //    var customer = _customersRepository.GetOneByID(id);
        //    model.Customer = customer;

        //    return View(model);
        //}

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