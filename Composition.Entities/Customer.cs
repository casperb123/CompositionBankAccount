using System;
using System.Collections.Generic;
using System.Text;

namespace Composition.Entities
{
    public class Customer
    {
        private int id;
        private List<Account> accounts = new List<Account>();

        public Customer(List<Account> accounts)
        {
            Accounts = accounts;
        }

        public Customer(int id, List<Account> accounts) : this(accounts)
        {
            Id = id;
        }

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

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

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

        public decimal GetTotalBalance()
        {
            return GetAssets() + GetDebts();
        }
    }
}
