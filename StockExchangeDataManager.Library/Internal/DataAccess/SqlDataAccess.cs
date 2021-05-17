using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeDataManager.Library.Internal.DataAccess
{
    internal class  SqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }
        public string GetconnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }

        public List<T> LoadData<T,U> (string storedProcedure,U parameters, string connectionStringName)
        {
            string connectionString = GetconnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }
        public void SaveData <T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetconnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                
            }
        }

    }
}
