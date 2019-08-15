using Composition.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Composition.UnitTests
{
    public class CustomerTests
    {
        [Fact]
        public void NewCustomerTest()
        {
            Account account = new Account(100);
            Account account1 = new Account(100);
            List<Account> accounts = new List<Account>() { account, account1 };

            Assert.True(new Customer(accounts) is Customer);
        }

        [Fact]
        public void NullNewCustomerTest()
        {
            List<Account> accounts = null;

            Assert.Throws<ArgumentNullException>(() => new Customer(accounts));
        }

        [Fact]
        public void GetDebtsTest()
        {
            Account account = new Account(1000000);
            Account account1 = new Account(-10000);
            List<Account> accounts = new List<Account>() { account, account1 };
            Customer customer = new Customer(accounts);

            Assert.True(customer.GetDebts() == -10000);
        }

        [Fact]
        public void GetAssetsTest()
        {
            Account account = new Account(1000000);
            Account account1 = new Account(-10000);
            List<Account> accounts = new List<Account>() { account, account1 };
            Customer customer = new Customer(accounts);

            Assert.True(customer.GetAssets() == 1000000);
        }

        [Fact]
        public void GetTotalBalanceTest()
        {
            Account account = new Account(1000);
            Account account1 = new Account(-1000);
            List<Account> accounts = new List<Account>() { account, account1 };
            Customer customer = new Customer(accounts);

            Assert.True(customer.GetTotalBalance() == 0);
        }

        [Theory]
        [InlineData(-2_700_000, 1_300_000)]
        [InlineData(-2_400_000, 1_000_000)]
        public void Rating1Test(decimal debt, decimal assets)
        {
            Account account = new Account(debt);
            Account account1 = new Account(assets);
            List<Account> accounts = new List<Account>() { account, account1 };
            Customer customer = new Customer(accounts);

            Assert.True(customer.Rating == 1);
        }

        [Theory]
        [InlineData(-2_700_000, 200_000)]
        [InlineData(-2_400_000, 30_000)]
        public void Rating2Test(decimal debt, decimal assets)
        {
            Account account = new Account(debt);
            Account account1 = new Account(assets);
            List<Account> accounts = new List<Account>() { account, account1 };
            Customer customer = new Customer(accounts);

            Assert.True(customer.Rating == 2);
        }

        [Theory]
        [InlineData(-1_000_000, 1_000_000)]
        [InlineData(-100_000, 20_000)]
        public void Rating3Test(decimal debt, decimal assets)
        {
            Account account = new Account(debt);
            Account account1 = new Account(assets);
            List<Account> accounts = new List<Account>() { account, account1 };
            Customer customer = new Customer(accounts);

            Assert.True(customer.Rating == 3);
        }

        [Theory]
        [InlineData(-10_000, 25_000)]
        [InlineData(-100_000, 25_000)]
        public void Rating4Test(decimal debt, decimal assets)
        {
            Account account = new Account(debt);
            Account account1 = new Account(assets);
            List<Account> accounts = new List<Account>() { account, account1 };
            Customer customer = new Customer(accounts);

            Assert.True(customer.Rating == 4);
        }

        [Theory]
        [InlineData(-200_000, 25_000)]
        [InlineData(-10_000, 40_000)]
        public void Rating5Test(decimal debt, decimal assets)
        {
            Account account = new Account(debt);
            Account account1 = new Account(assets);
            List<Account> accounts = new List<Account>() { account, account1 };
            Customer customer = new Customer(accounts);

            Assert.True(customer.Rating == 5);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-3_000_000, 1_300_000)]
        public void InvalidRatingTest(decimal debt, decimal assets)
        {
            Account account = new Account(debt);
            Account account1 = new Account(assets);
            List<Account> accounts = new List<Account>() { account, account1 };
            Customer customer = new Customer(accounts);

            Assert.Throws<InvalidOperationException>(() => customer.Rating == 5);
        }
    }
}
