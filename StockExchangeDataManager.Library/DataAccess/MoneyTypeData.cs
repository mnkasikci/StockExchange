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
    public class MoneyTypeData
    {
        private readonly IConfiguration _config;

        public MoneyTypeData(IConfiguration config)
        {
            _config = config;
        }
        public async Task AddPendingMoney(AddPendingMoneyModel moneyModel)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            await sql.SaveData<AddPendingMoneyModel>("dbo.[spAddMoneyToAuthorize]", moneyModel, "StockExchangeData");
        }

        public async Task<decimal> GetUserMoneyByID(string userID)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { @UserID = userID };
            var outputlist = await sql.LoadDataAsync<decimal, dynamic>("spGetUserMoneyByID", p, "StockExchangeData");
            return outputlist.First();
        }
        //spGetUserMoneyByID
        // @UserID nvarchar(128)
    }


}