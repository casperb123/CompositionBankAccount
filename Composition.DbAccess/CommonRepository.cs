using System;
using System.Data;
using System.Data.SqlClient;

namespace Composition.DbAccess
{
    public class CommonRepository
    {
        protected string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Bank;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// Executes the given sql string and returns a datatable
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <returns>DataTable</returns>
        protected DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand(query, con))
            using (SqlDataAdapter dap = new SqlDataAdapter(com))
            {
                dap.Fill(dataTable);
            }

            return dataTable;
        }

        /// <summary>
        /// Executes the sql query and returns the amount of rows affected
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <returns>Rows affected</returns>
        protected int ExecuteNonQuery(string query)
        {
            int rowsAffected = 0;

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand(query, con))
            {
                con.Open();

                rowsAffected = com.ExecuteNonQuery();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Executes the sql query and returns the output
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <returns>The output from the query</returns>
        protected int ExecuteNonQueryScalar(string query)
        {
            int number = 0;

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand(query, con))
            {
                con.Open();

                number = (int)com.ExecuteScalar();
            }

            return number;
        }

        /// <summary>
        /// Inserts the DataTable into the database
        /// </summary>
        /// <param name="dataTable">The DataTable to insert</param>
        /// <param name="destination">The table to insert into</param>
        protected void BulkInsert(DataTable dataTable, string destination)
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(conString))
            {
                sqlBulkCopy.BulkCopyTimeout = 600;
                sqlBulkCopy.DestinationTableName = destination;
                sqlBulkCopy.WriteToServer(dataTable);
            }
        }
    }
}
