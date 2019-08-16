using Composition.DbAccess;
using Composition.Entities;
using System;
using System.Collections.Generic;

namespace Composition
{
    class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            List<Customer> customers = customerRepository.GetCustomers();

            foreach (Customer customer in customers)
            {
                try
                {
                    Console.WriteLine($"ID: {customer.Id}\n" +
                                      $"Rating: {customer.Rating}\n" +
                                      $"Assets: {customer.GetAssets()}\n" +
                                      $"Debts: {customer.GetDebts()}\n" +
                                      $"Total Balance: {customer.GetTotalBalance()}\n");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"ID: {customer.Id}\n" +
                                      $"Rating: {ex.Message}\n" +
                                      $"Assets: {customer.GetAssets()}\n" +
                                      $"Debts: {customer.GetDebts()}\n" +
                                      $"Total Balance: {customer.GetTotalBalance()}\n");
                }

                Console.WriteLine("=====Accounts=====");

                foreach (Account account in customer.Accounts)
                {
                    Console.WriteLine($"ID: {account.Id}\n" +
                                      $"Balance: {account.Balance}\n" +
                                      $"Created: {account.Created.ToLongDateString()} {account.Created.ToLongTimeString()}\n" +
                                      $"Days since creation: {account.GetDaysSinceCreated()}\n");
                }

                Console.WriteLine();
            }

            Console.ReadKey(true);
        }
    }
}
