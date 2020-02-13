using System;
using System.Data;
using System.Data.SqlClient;

namespace cduff.EmailProcessor.Data
{
    /// <summary>
    /// Data Access Class that connects to the database.
    /// </summary>
    public class DataAccessBase
    {
        private static SqlConnection sqlConnection;

        public DataAccessBase(string connectionString)
        {
            InitializeDatabaseConnection(connectionString);
        }

        /// <summary>
        /// Initializes the connection to the SQL database.
        /// </summary>
        private void InitializeDatabaseConnection(string connectionString)
        {
            try
            {
                // TODO: fix connection logic, transaction scopes?
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                Exception exception = new Exception("Error Connecting to the Database.", ex);
                throw exception;
            }
        }

        public DataTable FillDataTable(string storedProcedure, object[] parameters)
        {
            // TODO: write new fill logic
            DataTable result = new DataTable();
            // storageAgent.Fill(result, storedProcedure, parameters);
            return result;
        }

        public bool UpdateDataTable(string storedProcedure, DataTable dataTable)
        {
            try
            {
                // TODO: write new insert logic
                // storageAgent.Write(dataTable, storedProcedure);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
