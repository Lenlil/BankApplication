using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Data;
using Bank.Interfaces;
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

        public CustomerController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IAccountsRepository accountRepository,
            ICustomersRepository customersRepository)
        {
            _logger = logger;
            this.dbContext = dbContext;
            _accountsRepository = accountRepository;
            _customersRepository = customersRepository;
        }
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShowCustomer(SearchViewModel model)
        {
            bool ok = true;

            if (_productRepository.GetAllProducts().Any(p => p.Name == model.ProductName))
            {
                ModelState.AddModelError(string.Empty, "Product is already exists with this name.");
                ok = false;
            }
            if (!ModelState.IsValid || !ok)
            {
                ModelState.AddModelError(string.Empty, "Please Fill all the fields.");
                model.MenuItems = SetupMenu("AddProduct");

                var categoryList = _categoryRepo.GetAllCategories();
                model.Categories = categoryList.Select(x =>
                                      new SelectListItem()
                                      {
                                          Text = x.CategoryName.ToString(),
                                          Value = x.CategoryId.ToString()

                                      });
                var suppliersList = _suppliersRepository.GetAllSuppliers();
                model.Suppliers = suppliersList.Select(x =>
                                     new SelectListItem()
                                     {
                                         Text = x.CompanyName.ToString(),
                                         Value = x.SupplierId.ToString()

                                     });
                return View(model);
            }

            var newProduct = new Products()
            {
                ProductName = model.ProductName,
                UnitPrice = model.UnitPrice,
                UnitsInStock = model.UnitsInStock,
                FirstSaleDate = model.FirstSaleDate,
                QuantityPerUnit = model.QuantityPerUnit,
                CategoryId = model.CategoryId,
                SupplierId = model.SupplierId,
                Discontinued = model.Discontinued,
            };

            _productRepository.CreateProduct(newProduct);


            return RedirectToAction("Products");
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