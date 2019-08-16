using Composition.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Composition.DbAccess
{
    public class CustomerRepository : CommonRepository
    {
        /// <summary>
        /// Returns all of the customers from the database
        /// </summary>
        /// <returns>List of customers</returns>
        public List<Customer> GetCustomers()
        {
            AccountRepository accountRepository = new AccountRepository();
            List<Customer> customers = new List<Customer>();
            DataTable dataTable = ExecuteQuery("SELECT * FROM Customers");

            foreach (DataRow row in dataTable.Rows)
            {
                int id = (int)row["Id"];
                List<Account> accounts = accountRepository.GetAccountsForCustomer(id);
                Customer customer = new Customer(id, accounts);
                customers.Add(customer);
            }

            return customers;
        }

        /// <summary>
        /// Returns a customer with the given id
        /// </summary>
        /// <param name="id">The id of the customer</param>
        /// <returns>Customer</returns>
        public Customer GetCustomer(int id)
        {
            AccountRepository accountRepository = new AccountRepository();
            DataTable dataTable = ExecuteQuery($"SELECT * FROM Customers WHERE Id = {id}");

            if (dataTable.Rows.Count < 1)
            {
                throw new IndexOutOfRangeException($"A customer with the id of {id} doesn't exist");
            }

            DataRow row = dataTable.Rows[0];
            List<Account> accounts = accountRepository.GetAccountsForCustomer(id);

            return new Customer(id, accounts);
        }

        /// <summary>
        /// Inserts a customer into the database
        /// </summary>
        /// <param name="id">The id of the customer</param>
        /// <returns>The customers new id</returns>
        public int InsertCustomer(int id)
        {
            return ExecuteNonQueryScalar($"INSERT INTO Customers (Id) OUTPUT INSERTED.Id VAULUES ({id})");
        }
    }
}
