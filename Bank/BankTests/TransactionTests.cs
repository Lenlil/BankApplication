using Bank.Data;
using Bank.Interfaces;
using Bank.Services;
using Bank.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;


namespace BankTests
{
    [TestClass]
    public class TransactionTests
    {        
         TransactionServices sut;

         Mock<IAccountsRepository> accountsRepositoryMock;
         Mock<IDispositionsRepository> dispositionsRepositoryMock;
         Mock<ITransactionsRepository> transactionsRepositoryMock;
         Mock<ViewModelsService> viewmodelsServicesMock;
         Mock<AccountServices> accountServicesMock;       

        public TransactionTests()
        {
            accountsRepositoryMock = new Mock<IAccountsRepository>();
            dispositionsRepositoryMock = new Mock<IDispositionsRepository>();
            transactionsRepositoryMock = new Mock<ITransactionsRepository>();            
            accountServicesMock = new Mock<AccountServices>(accountsRepositoryMock.Object, dispositionsRepositoryMock.Object, transactionsRepositoryMock.Object);
            viewmodelsServicesMock = new Mock<ViewModelsService>(accountsRepositoryMock.Object, accountServicesMock.Object);

            sut = new TransactionServices(accountsRepositoryMock.Object, dispositionsRepositoryMock.Object, transactionsRepositoryMock.Object, viewmodelsServicesMock.Object, accountServicesMock.Object);
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

        //[TestMethod]
        //public void When_transferring_inside_bank_cant_take_more_money_than_balance_on_account()
        //{

        //}

        //[TestMethod]
        //public void When_transferring_outside_bank_cant_take_more_money_than_balance_on_account()
        //{

        //}

        //[TestMethod]
        //public void When_depositing_cannot_enter_negative_amount()
        //{

        //}

        //[TestMethod]
        //public void When_withdrawing_cannot_enter_negative_amount()
        //{

        //}

        //[TestMethod]
        //public void When_transactions_is_created_it_is_ok()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()

        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())

        //        .EnableSensitiveDataLogging()

        //        .Options;

        //    var ctx = new NorthwindContext(options);

        //    sut = new TransactionServices();

        //    var transaction = fixture.Create(Transactions);
        //}
    }
}
