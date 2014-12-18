using System.Collections.Generic;
using System.Data.SqlClient;
using ConnCar.Data.Contracts;
using Microsoft.Samples.ObjectDataReader;

namespace ConnCar.Data.EF.BaseRepoDB.Helpers
{
    /// <summary>
    /// Basic implementation of the bulk copy manager with EF and SQL Server
    /// Note: this implementation is proper to MSSQL server and won't work with other DBs
    /// </summary>
    public class SqlBulkCopyManager : IBulkCopyManager
    {
        private readonly string _connectionString;

        public SqlBulkCopyManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void TruncateTable(string tableName)
        {
            using (var con = new SqlConnection(this._connectionString))
            {
                con.Open();

                var cmd = new SqlCommand(@"Truncate table " + tableName, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void BulkInsert<T>(string tableName, List<T> entities)
        {
            var bulkCopy = new SqlBulkCopy(_connectionString)
            {
                DestinationTableName = tableName,
                NotifyAfter = 1000
            };
            var table = entities.AsDataReader();
            bulkCopy.WriteToServer(table);
            bulkCopy.Close();
        }
    }
}
