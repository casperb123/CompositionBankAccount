using System;
using System.Collections.Generic;
using System.Text;

namespace Composition.Entities
{
    public class Customer
    {
        private int id;
        private List<Account> accounts;

        /// <summary>
        /// Creates a new instance of this class. Use this for new customers
        /// </summary>
        /// <param name="accounts">The accounts to add to the customer</param>
        public Customer(List<Account> accounts)
        {
            Accounts = accounts;
        }

        /// <summary>
        /// Creates a new instance of this class. Use this for existing customers
        /// </summary>
        /// <param name="id">The id of the customer</param>
        /// <param name="accounts">The accounts to add to the customer</param>
        public Customer(int id, List<Account> accounts) : this(accounts)
        {
            Id = id;
        }

        /// <summary>
        /// Returns the rating for the customer
        /// </summary>
        public int Rating
        {
            get
            {
                decimal debt = GetDebts();
                decimal assets = GetAssets();

                if (debt <= -2_250_000 && assets > 1_250_000)
                {
                    return 1;
                }
                else if (debt <= -2_250_000 && assets >= 50_000 && assets <= 1_250_000)
                {
                    return 2;
                }
                else if (debt <= -250_000 && debt >= -2_250_000 && assets >= 50_000 && assets <= 1_250_000)
                {
                    return 3;
                }
                else if (debt < 0 && debt > -250_000 && assets > 0 && assets < 50_000)
                {
                    if (debt + assets < 0)
                    {
                        return 5;
                    }

                    return 4;
                }
                else
                {
                    throw new InvalidOperationException("Couldn't get any rating");
                }
            }
        }

        /// <summary>
        /// Returns a list of the customers accounts
        /// </summary>
        public List<Account> Accounts
        {
            get { return accounts; }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("The accounts list can't be null");
                }

                accounts = value;
            }
        }

        /// <summary>
        /// Returns the customers id
        /// </summary>
        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("The id can't be lower than 0");
                }

                id = value;
            }
        }

        /// <summary>
        /// Returns the customers debts
        /// </summary>
        /// <returns>The customers debts</returns>
        public decimal GetDebts()
        {
            decimal debt = 0;

            foreach (Account account in Accounts)
            {
                if (account.Balance < 0)
                {
                    debt += account.Balance;
                }
            }

            return debt;
        }

        /// <summary>
        /// Returns the customers assets
        /// </summary>
        /// <returns>The customers assets</returns>
        public decimal GetAssets()
        {
            decimal asset = 0;

            foreach (Account account in Accounts)
            {
                if (account.Balance > 0)
                {
                    asset += account.Balance;
                }
            }

            return asset;
        }

        /// <summary>
        /// Returns the customers total balance
        /// </summary>
        /// <returns>The customers total balance</returns>
        public decimal GetTotalBalance()
        {
            return GetAssets() + GetDebts();
        }
    }
}
