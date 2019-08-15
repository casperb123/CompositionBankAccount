using Composition.Entities;
using System;
using Xunit;

namespace Composition.UnitTests
{
    public class AccountTests
    {
        [Theory]
        [InlineData(1000000)]
        [InlineData(999999999999)]
        [InlineData(-999999999999)]
        public void NewAccountTest(decimal balance)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Account(balance));
        }

        [Theory]
        [InlineData(1, 100000, "14/08/2019 11:22")]
        [InlineData(1, 999999999999, "14/08/2019 11:22")]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExistingAccountTest(int id, decimal balance, string created)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Account(id, balance, DateTime.Parse(created)));
        }

        [Theory]
        [InlineData(100)]
        [InlineData(-100)]
        [InlineData(26000)]
        public void WithdrawExceptionTest(decimal amount)
        {
            Account account = new Account(1000000);
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Withdraw(amount));
        }

        [Theory]
        [InlineData(10000)]
        public void DepositTest(decimal amount)
        {
            Account account = new Account(0);
            account.Deposit(amount);
            Assert.True(account.Balance == amount);
        }

        [Theory]
        [InlineData(10000)]
        public void BalanceExceptionTest(decimal amount)
        {
            Account account = new Account(0);
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Balance += amount);
        }
    }
}
