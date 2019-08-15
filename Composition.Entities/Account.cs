using System;

namespace Composition.Entities
{
    public class Account
    {
        private int id;
        private decimal balance;
        private DateTime created;

        /// <summary>
        /// Initializes a new instance on this class. Use this for a new account
        /// </summary>
        /// <param name="initialBalance">The initial account balance</param>
        public Account(decimal initialBalance)
        {
            Id = 0;
            Balance = initialBalance;
            Created = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of this class. Use this if it's an existing account
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="balance">The account balance</param>
        /// <param name="created">The date the account was created</param>
        public Account(int id, decimal balance, DateTime created)
        {
            Id = id;
            Balance = balance;
            Created = created;
        }

        /// <summary>
        /// The date the account were created
        /// </summary>
        public DateTime Created
        {
            get { return created; }
            set { created = value; }
        }

        /// <summary>
        /// The balance of the account
        /// </summary>
        public decimal Balance
        {
            get { return balance; }
            set
            {
                if (value > 99999999999 || value < -99999999999)
                {
                    throw new ArgumentOutOfRangeException("The balance has to be between -999.999.999,99 and 999.999.999,99");
                }

                balance = value;
            }
        }

        /// <summary>
        /// The id of the account
        /// </summary>
        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("The id can't be lower than 1");
                }

                id = value;
            }
        }

        /// <summary>
        /// Withdraws the given amount from the balance
        /// </summary>
        /// <param name="amount">The amount to withdraw</param>
        public void Withdraw(decimal amount)
        {
            if (amount < 0 || amount > 25000)
            {
                throw new ArgumentOutOfRangeException("The amount has to bee between 0 and 25.000");
            }

            Balance -= amount;
        }

        /// <summary>
        /// Deposits the given amount to the balance
        /// </summary>
        /// <param name="amount">The amount to deposit</param>
        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        /// <summary>
        /// Returns the amount of days since the account were created
        /// </summary>
        /// <returns>Days since creation</returns>
        public int GetDaysSinceCreated()
        {
            return (DateTime.Now - Created).Days;
        }
    }
}
