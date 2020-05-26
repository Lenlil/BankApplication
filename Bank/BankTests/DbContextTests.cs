using AutoFixture;
using Bank.Data;
using Bank.Models;
using Bank.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankTests
{
    class DbContextTests
    {
        ApplicationDbContext ctx;

        private ApplicationDbContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;
            ctx = new ApplicationDbContext(options);

            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            ctx.Accounts.Add(fixture.Create<Accounts>());
            ctx.Accounts.Add(fixture.Create<Accounts>());

            ctx.SaveChanges();
            return ctx;
        }

        [TestMethod]
        public void Withdrawal_transaction_is_created_ok()
        {
            var ctx = GetContextWithData();

            var model = new AddTransactionViewModel
            {
                Date = DateTime.Now,
                OldAccountBalance = 1000,
                Type = "Debit",
                Operation = "Withdrawal in Cash",
                Amount = 2000,
                FromAccountId = 1,
            };

            var accounts = ctx.Accounts;
                        
        }
    }
}
