using AutoFixture;
using Bank.Data;
using Bank.Interfaces;
using Bank.Models;
using Bank.Repositories;
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
         AccountsDbRepository accountsRepository;
         DispositionsDbRepository dispositionsRepository;
         TransactionsDbRepository transactionsRepository;        
         AccountServices accountServices;

         ApplicationDbContext ctx;

        public TransactionServiceTests()
        {
            ctx = GetContextWithData();
            accountsRepository = new AccountsDbRepository(ctx);
            dispositionsRepository = new DispositionsDbRepository(ctx);
            transactionsRepository = new TransactionsDbRepository(ctx);            
            accountServices = new AccountServices(accountsRepository, dispositionsRepository, transactionsRepository);            

            sut = new TransactionServices(accountsRepository, dispositionsRepository, transactionsRepository, accountServices);
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
                Date = DateTime.Now.Date,
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
                Type = model.Type,
                Operation = model.Operation,
                Amount = -model.Amount,
                Symbol = model.Symbol,
                Bank = model.Bank,
                Account = model.ToAccount
            };
            
            sut.CreateWithdrawalTransaction(model);

            IQueryable<Transactions> transactions = ctx.Transactions;
            var transactionID = transactions.Max(x => x.TransactionId);
            var createdTransaction = transactions.Where(x => x.TransactionId == transactionID).FirstOrDefault();

            Assert.AreEqual(expectedTransaction.AccountId, createdTransaction.AccountId);
            Assert.AreEqual(expectedTransaction.Date, createdTransaction.Date);
            Assert.AreEqual(expectedTransaction.Balance, createdTransaction.Balance);
            Assert.AreEqual(expectedTransaction.Type, createdTransaction.Type);
            Assert.AreEqual(expectedTransaction.Operation, createdTransaction.Operation);
            Assert.AreEqual(expectedTransaction.Amount, createdTransaction.Amount);
            Assert.AreEqual(expectedTransaction.Symbol, createdTransaction.Symbol);
            Assert.AreEqual(expectedTransaction.Bank, createdTransaction.Bank);
            Assert.AreEqual(expectedTransaction.Account, createdTransaction.Account);
        }

        [TestMethod]
        public void Deposit_transaction_is_created_ok()
        {
            List<Accounts> accounts = ctx.Accounts.ToList();
            var fromAccount = accounts[0];
            fromAccount.Balance = 1000;
            ctx.Accounts.Update(fromAccount);
            ctx.SaveChanges();

            var model = new AddTransactionViewModel
            {
                Date = DateTime.Now.Date,
                OldAccountBalance = fromAccount.Balance,
                Type = "Credit",
                Operation = "Credit in Cash",
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
                Balance = model.OldAccountBalance + model.Amount,
                Type = model.Type,
                Operation = model.Operation,
                Amount = model.Amount,
                Symbol = model.Symbol,
                Bank = model.Bank,
                Account = model.ToAccount
            };

            sut.CreateDepositTransaction(model);

            IQueryable<Transactions> transactions = ctx.Transactions;
            var transactionID = transactions.Max(x => x.TransactionId);
            var createdTransaction = transactions.Where(x => x.TransactionId == transactionID).FirstOrDefault();

            Assert.AreEqual(expectedTransaction.AccountId, createdTransaction.AccountId);
            Assert.AreEqual(expectedTransaction.Date, createdTransaction.Date);
            Assert.AreEqual(expectedTransaction.Balance, createdTransaction.Balance);
            Assert.AreEqual(expectedTransaction.Type, createdTransaction.Type);
            Assert.AreEqual(expectedTransaction.Operation, createdTransaction.Operation);
            Assert.AreEqual(expectedTransaction.Amount, createdTransaction.Amount);
            Assert.AreEqual(expectedTransaction.Symbol, createdTransaction.Symbol);
            Assert.AreEqual(expectedTransaction.Bank, createdTransaction.Bank);
            Assert.AreEqual(expectedTransaction.Account, createdTransaction.Account);
        }

        [TestMethod]
        public void Transfer_within_bank_transactions_are_created_ok()
        {
            List<Accounts> accounts = ctx.Accounts.ToList();
            var fromAccount = accounts[0];
            fromAccount.Balance = 1000;
            ctx.Accounts.Update(fromAccount);
            ctx.SaveChanges();

            var toAccount = accounts[1];
            var oldToAccountBalance = toAccount.Balance;

            var model = new TransferThisBankTransactionViewModel
            {
                Date = DateTime.Now.Date,
                OldAccountBalance = fromAccount.Balance,
                Type = "Debit",
                Operation = "Remittance to Another Account",
                Amount = 500,
                FromAccountId = fromAccount.AccountId,
                Symbol = null,
                Bank = "Bitcoin Bank",
                ToAccountId = toAccount.AccountId
            };

            var expectedFromTransaction = new Transactions
            {
                AccountId = model.FromAccountId,
                Date = model.Date,
                Balance = model.OldAccountBalance - model.Amount,
                Type = model.Type,
                Operation = model.Operation,
                Amount = -model.Amount,
                Symbol = model.Symbol,
                Bank = model.Bank,
                Account = toAccount.AccountId.ToString()
            };

            var expectedToTransaction = new Transactions
            {
                AccountId = model.ToAccountId,
                Date = model.Date,
                Balance = oldToAccountBalance + model.Amount,
                Type = "Credit",
                Operation = "Collection from Another Account",
                Amount = model.Amount,
                Symbol = model.Symbol,
                Bank = model.Bank,
                Account = model.FromAccountId.ToString()
            };

            sut.CreateTransferThisBankFromAccountTransaction(model);
            sut.CreateTransferThisBankToAccountTransaction(model);

            IQueryable<Transactions> transactions = ctx.Transactions;
            var toTransactionID = transactions.Max(x => x.TransactionId);
            var createdToTransaction = transactions.Where(x => x.TransactionId == toTransactionID).FirstOrDefault();

            var fromTransactionID = toTransactionID - 1;
            var createdFromTransaction = transactions.Where(x => x.TransactionId == fromTransactionID).FirstOrDefault();                    
                       
            Assert.AreEqual(expectedFromTransaction.AccountId, createdFromTransaction.AccountId);
            Assert.AreEqual(expectedFromTransaction.Date, createdFromTransaction.Date);
            Assert.AreEqual(expectedFromTransaction.Balance, createdFromTransaction.Balance);
            Assert.AreEqual(expectedFromTransaction.Type, createdFromTransaction.Type);
            Assert.AreEqual(expectedFromTransaction.Operation, createdFromTransaction.Operation);
            Assert.AreEqual(expectedFromTransaction.Amount, createdFromTransaction.Amount);
            Assert.AreEqual(expectedFromTransaction.Symbol, createdFromTransaction.Symbol);
            Assert.AreEqual(expectedFromTransaction.Bank, createdFromTransaction.Bank);
            Assert.AreEqual(expectedFromTransaction.Account, createdFromTransaction.Account);

            Assert.AreEqual(expectedToTransaction.AccountId, createdToTransaction.AccountId);
            Assert.AreEqual(expectedToTransaction.Date, createdToTransaction.Date);
            Assert.AreEqual(expectedToTransaction.Balance, createdToTransaction.Balance);
            Assert.AreEqual(expectedToTransaction.Type, createdToTransaction.Type);
            Assert.AreEqual(expectedToTransaction.Operation, createdToTransaction.Operation);
            Assert.AreEqual(expectedToTransaction.Amount, createdToTransaction.Amount);
            Assert.AreEqual(expectedToTransaction.Symbol, createdToTransaction.Symbol);
            Assert.AreEqual(expectedToTransaction.Bank, createdToTransaction.Bank);
            Assert.AreEqual(expectedToTransaction.Account, createdToTransaction.Account);
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
