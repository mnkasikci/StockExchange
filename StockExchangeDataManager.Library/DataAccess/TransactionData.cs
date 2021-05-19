using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockExchangeDataManager.Library.Internal.DataAccess;
using StockExchangeDataManager.Library.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeDataManager.Library.DataAccess
{
    public class TransactionData
    {
        private readonly IConfiguration _config;

        public TransactionData(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<TransactionModel>> GetAllTransactions()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { };
            var output = await sql.LoadDataAsync<TransactionModel, dynamic>("dbo.spGetAllTransactions", p, "StockExchangeData");
            return output;
        }
        public async Task<List<TransactionModel>> GetTransactionsByID(string UserId)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { UserId = UserId };
            var output = await sql.LoadDataAsync<TransactionModel, dynamic>("dbo.spGetTransactionsByID", p, "StockExchangeData");
            return output;
        }
    }
}
