using Composition.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Composition.DbAccess
{
    public class AccountRepository : CommonRepository
    {
        /// <summary>
        /// Returns all of the accounts from the database
        /// </summary>
        /// <returns>List of accounts</returns>
        public List<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account>();
            DataTable dataTable = ExecuteQuery("SELECT * FROM BankAccounts");

            foreach (DataRow row in dataTable.Rows)
            {
                Account account = new Account((int)row["Id"], (decimal)row["Balance"], (DateTime)row["Created"]);
                accounts.Add(account);
            }

            return accounts;
        }

        /// <summary>
        /// Returns an account with the given id
        /// </summary>
        /// <param name="id">The customers id</param>
        /// <returns>List of accounts</returns>
        public Account GetAccount(int id)
        {
            DataTable dataTable = ExecuteQuery($"SELECT * FROM BankAccounts WHERE Id = {id}");

            if (dataTable.Rows.Count < 1)
            {
                throw new IndexOutOfRangeException($"An account with the id of {id} doesn't exist");
            }

            DataRow row = dataTable.Rows[0];

            return new Account((int)row["Id"], (decimal)row["Balance"], (DateTime)row["Created"]);
        }

        /// <summary>
        /// Returns all of the accounts for a customer
        /// </summary>
        /// <param name="customerId">The customers id</param>
        /// <returns>List of accounts</returns>
        public List<Account> GetAccountsForCustomer(int customerId)
        {
            string query = $"SELECT " +
                           $"BankAccounts.Id," +
                           $"BankAccounts.Balance," +
                           $"BankAccounts.Created " +
                           $"FROM BankAccounts " +
                           $"INNER JOIN BankAccountsInCustomers as BIC ON BankAccounts.Id = BIC.AccountId " +
                           $"INNER JOIN Customers ON BIC.CustomerId = Customers.Id";

            List<Account> accounts = new List<Account>();
            DataTable dataTable = ExecuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                Account account = new Account((int)row["Id"], (decimal)row["Balance"], (DateTime)row["Created"]);
                accounts.Add(account);
            }

            return accounts;
        }

        /// <summary>
        /// Inserts an account into the database
        /// </summary>
        /// <param name="account">The account to insert</param>
        /// <returns></returns>
        public int InsertAccount(Account account)
        {
            return ExecuteNonQueryScalar($"INSERT INTO BankAccounts (Balance, Created) OUTPUT INSERTED.Id VALUES ({account.Balance}, '{account.Created.ToString("yyyy-MM-dd HH:mm:ss")}')");
        }

        /// <summary>
        /// Inserts the accounts into the database
        /// </summary>
        /// <param name="accounts">The accounts to insert</param>
        public void InsertAccounts(List<Account> accounts)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Balance", typeof(decimal));
            dataTable.Columns.Add("Created", typeof(DateTime));

            accounts.ForEach(x => dataTable.Rows.Add(x.Id, x.Balance, x.Created));

            BulkInsert(dataTable, "BankAccounts");
        }

        /// <summary>
        /// Deletes the account from the database
        /// </summary>
        /// <param name="account">The account to delete</param>
        /// <returns>The number of rows affected</returns>
        public void DeleteAccount(Account account)
        {
            int rowsAffected = ExecuteNonQuery($"DELETE FROM BankAccounts WHERE Id = {account.Id}");

            if (rowsAffected == 0)
            {
                throw new IndexOutOfRangeException("That account doesn't exist");
            }
        }

        /// <summary>
        /// Updates the account on the database
        /// </summary>
        /// <param name="account">The account to update</param>
        /// <returns>The number of rows affected</returns>
        public int UpdateAccount(Account account)
        {
            int rowsAffected = ExecuteNonQuery($"UPDATE BankAccounts SET Balance = {account.Balance} WHERE Id = {account.Id}");

            if (rowsAffected == 0)
            {
                throw new IndexOutOfRangeException("That account doesn't exist");
            }

            return rowsAffected;
        }

        /// <summary>
        /// Adds the account to the customer
        /// </summary>
        /// <param name="account">The account to add</param>
        /// <param name="customerId">The customers id</param>
        /// <returns>The number of rows affected</returns>
        public int AddAccountToCustomer(Account account, int customerId)
        {
            string query = $"INSERT INTO BankAccountsInCustomers " +
                           $"(AccountId, CustomerId) " +
                           $"VALUES " +
                           $"({account.Id}, {customerId})";

            int rowsAffected = ExecuteNonQuery(query);

            if (rowsAffected == 0)
            {
                throw new Exception("The account already exists in that customer");
            }

            return rowsAffected;
        }
    }
}
