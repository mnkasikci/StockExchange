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
            if (outputlist.Count == 0)
                return 0;
            return outputlist.First();
        }
        public async Task<List<PendingMoneyModel>> GetAllPendingMoneys()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { };
            var output = await sql.LoadDataAsync<PendingMoneyModel, dynamic>("dbo.spGetAllPendingMoneys", p, "StockExchangeData");
            return output;

        }
        public async Task RefusePendingMoney(int pendingId, string userID)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new
            {
                PendingMoneyID = pendingId,
                RefuserID = userID
            };
            await sql.SaveData<dynamic>("dbo.[spRefusePendingMoney]", p, "StockExchangeData");
        }
        public async Task AuthorizePendingMoney(int pendingMoneyID, string userID)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new
            {
                PendingMoneyID = pendingMoneyID,
                AuthorizerID = userID
            };
            await sql.SaveData<dynamic>("dbo.[spAuthorizePendingMoney]", p, "StockExchangeData");
        }

        public async Task CreateBuyOffer(OfferModel offer)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            await sql.SaveData<dynamic>("dbo.[spCreateBuyOffer]", offer, "StockExchangeData");
        }
    }


}