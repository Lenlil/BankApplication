using AutoFixture;
using Bank.Data;
using Bank.Interfaces;
using Bank.Models;
using Bank.Services;
using Bank.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankTests
{
    [TestClass]
    public class TransactionServiceTests
    {        
         TransactionServices sut;
         Mock<IAccountsRepository> accountsRepositoryMock;
         Mock<IDispositionsRepository> dispositionsRepositoryMock;
         Mock<ITransactionsRepository> transactionsRepositoryMock;        
         Mock<AccountServices> accountServicesMock;

         ApplicationDbContext ctx;

        public TransactionServiceTests()
        {
            ctx = GetContextWithData();
            accountsRepositoryMock = new Mock<IAccountsRepository>();
            dispositionsRepositoryMock = new Mock<IDispositionsRepository>();
            transactionsRepositoryMock = new Mock<ITransactionsRepository>();            
            accountServicesMock = new Mock<AccountServices>(accountsRepositoryMock.Object, dispositionsRepositoryMock.Object, transactionsRepositoryMock.Object);            

            sut = new TransactionServices(accountsRepositoryMock.Object, dispositionsRepositoryMock.Object, transactionsRepositoryMock.Object, accountServicesMock.Object);
        }

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
            List<Accounts> accounts = ctx.Accounts.ToList();

            var fromAccount = accounts[0];

            fromAccount.Balance = 1000;
            ctx.Accounts.Update(fromAccount);            
            ctx.SaveChanges();           

            var model = new AddTransactionViewModel
            {
                Date = DateTime.Now,
                OldAccountBalance = fromAccount.Balance,
                Type = "Debit",
                Operation = "Withdrawal in Cash",
                Amount = 500,
                FromAccountId = fromAccount.AccountId,
                Symbol = null,
                Bank = null,
                ToAccount = null
            };            

            var expectedTransaction = new Transactions
            {                 
                AccountId = model.FromAccountId,
                Date = model.Date,
                Balance = model.OldAccountBalance - model.Amount,
                Type = "Debit",
                Operation = "Withdrawal in Cash",
                Amount = model.Amount,
                Symbol = model.Symbol,
                Bank = model.Bank,
                Account = model.ToAccount
            };

            //Hur får jag in ctx i mitt mock-repository??
            sut.CreateWithdrawalTransaction(model);

            List<Transactions> transactions = ctx.Transactions.ToList();
            var createdTransaction = transactions[0];

            Assert.AreEqual(expectedTransaction, createdTransaction);
        }


        [TestMethod]
        public void When_withdrawing_can_not_take_more_money_than_balance_on_account()
        {
            var model = new AddTransactionViewModel { 
                Date = DateTime.Now, 
                OldAccountBalance = 1000,                 
                Type = "Debit",
                Operation = "Withdrawal in Cash",
                Amount = 2000,
                FromAccountId = 1,
            };

            var expectedErrorMessage = "Insufficient funds on account to perform the transaction.";

            var newModel = sut.CheckWithdrawalTransactionModelIsOkAndReturnViewmodel(model);

            Assert.AreEqual(expectedErrorMessage, newModel.ErrorMessageViewModel.ErrorMessage);
        }

        [TestMethod]
        public void When_withdrawing_can_take_equal_money_to_balance_on_account()
        {
            var model = new AddTransactionViewModel
            {
                Date = DateTime.Now,
                OldAccountBalance = 1000,
                Type = "Debit",
                Operation = "Withdrawal in Cash",
                Amount = 1000,
                FromAccountId = 1,
            };

            var expectedErrorMessage = "";

            var newModel = sut.CheckWithdrawalTransactionModelIsOkAndReturnViewmodel(model);

            Assert.AreEqual(expectedErrorMessage, newModel.ErrorMessageViewModel.ErrorMessage);
        }

        [TestMethod]
        public void When_withdrawing_can_take_less_money_than_balance_on_account()
        {
            var model = new AddTransactionViewModel
            {
                Date = DateTime.Now,
                OldAccountBalance = 1000,
                Type = "Debit",
                Operation = "Withdrawal in Cash",
                Amount = 500,
                FromAccountId = 1,
            };

            var expectedErrorMessage = "";

            var newModel = sut.CheckWithdrawalTransactionModelIsOkAndReturnViewmodel(model);

            Assert.AreEqual(expectedErrorMessage, newModel.ErrorMessageViewModel.ErrorMessage);
        }

        [TestMethod]
        public void When_withdrawing_cannot_enter_negative_amount()
        {
            var model = new AddTransactionViewModel
            {
                Date = DateTime.Now,
                OldAccountBalance = 1000,
                Type = "Debit",
                Operation = "Withdrawal in Cash",
                Amount = -500,
                FromAccountId = 1,
            };

            var expectedErrorMessage = "The amount entered cannot be negative or 0.";

            var newModel = sut.CheckWithdrawalTransactionModelIsOkAndReturnViewmodel(model);

            Assert.AreEqual(expectedErrorMessage, newModel.ErrorMessageViewModel.ErrorMessage);
        }

        [TestMethod]
        public void When_transferring_inside_bank_cant_take_more_money_than_balance_on_account()
        {
            var model = new TransferThisBankTransactionViewModel
            {              
                Date = DateTime.Now,
                Type = "Debit",
                Operation = "Remittance to Another Account",
                FromAccountId = 1,
                OldAccountBalance = 1000,
                Bank = "Bitcoin Bank", 
                ToAccountId = 2, 
                Amount = 2000
            };

            var expectedErrorMessage = "Insufficient funds on account to perform the transaction.";

            var newModel = sut.CheckTransferThisBankModelIsOkAndReturnViewmodel(model);

            Assert.AreEqual(expectedErrorMessage, newModel.ErrorMessageViewModel.ErrorMessage);
        }

        [TestMethod]
        public void When_transferring_outside_bank_cant_take_more_money_than_balance_on_account()
        {
            var model = new TransferTransactionViewModel
            {
                Date = DateTime.Now,
                Type = "Debit",
                Operation = "Remittance to Another Bank",
                FromAccountId = 1,
                OldAccountBalance = 1000,
                Bank = "Bitcoin Bank",
                ToAccount = "1234",
                Amount = 2000
            };

            var expectedErrorMessage = "Insufficient funds on account to perform the transaction.";

            var newModel = sut.CheckTransferOtherBankModelIsOkAndReturnViewmodel(model);

            Assert.AreEqual(expectedErrorMessage, newModel.ErrorMessageViewModel.ErrorMessage);
        }

        [TestMethod]
        public void When_depositing_cannot_enter_negative_amount()
        {
            var model = new AddTransactionViewModel
            {
                Date = DateTime.Now,
                OldAccountBalance = 1000,
                Type = "Credit",
                Operation = "Credit in Cash",
                Amount = -500,
                FromAccountId = 1,
            };

            var expectedErrorMessage = "The amount entered cannot be negative or 0.";

            var newModel = sut.CheckDepositTransactionModelIsOkAndReturnViewmodel(model);

            Assert.AreEqual(expectedErrorMessage, newModel.ErrorMessageViewModel.ErrorMessage);
        }


    }
}
