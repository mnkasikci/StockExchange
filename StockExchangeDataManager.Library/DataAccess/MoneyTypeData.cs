using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StockExchangeDataManager.Library.Internal.DataAccess;
using StockExchangeDataManager.Library.Models;

using System;
using System.Collections.Generic;
using System.Data;
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
            var p = new DynamicParameters();
            p.Add("@UserID", userID);
            p.Add("@Usermoney", dbType: DbType.Decimal , direction: ParameterDirection.Output, precision: 10, scale: 2);
            
            await sql.GetOutputFromStoredProcedure("spGetUserMoneyByID", p, "StockExchangeData");
            var usermoney = p.Get<decimal>("@Usermoney");

            return usermoney;
        }
        public async Task<List<PendingMoneyModel>> GetAllPendingMoneys()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { };
            var output = await sql.LoadDataAsync<PendingMoneyModel, dynamic>("dbo.spGetAllPendingMoneys", p, "StockExchangeData");
            return output;

        }
        public record CurrencyType(string CurrencyCode, string CurrencyName);
        public async Task<List<CurrencyType>> GetAllCurrencyTypes()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { };
            var output = await sql.LoadDataAsync<CurrencyType, dynamic>("dbo.spGetCurrencyTypes", p, "StockExchangeData");
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
        public async Task AuthorizePendingMoney(int pendingMoneyID, string userID, decimal CurrencyRate)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new
            {
                PendingMoneyID = pendingMoneyID,
                AuthorizerID = userID,
                CurrencyRate = CurrencyRate
            };
            await sql.SaveData<dynamic>("dbo.[spAuthorizePendingMoney]", p, "StockExchangeData");
        }

        public async Task<PendingMoneyModel> GetPendingMoneyById(int pendingMoneyId)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { @PendingID = pendingMoneyId };
            var output = await sql.LoadDataAsync<PendingMoneyModel, dynamic>("dbo.spGetPendingMoneyById", p, "StockExchangeData");
            return output.First();
        }

        public async Task CreateBuyOffer(OfferModel offer)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            await sql.SaveData<dynamic>("dbo.[spCreateBuyOffer]", offer, "StockExchangeData");
        }

        public async Task<bool> MarketOrderPriceStillValid (MarketOrderModel marketOrder)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);


            var output = await sql.LoadDataAsync<int, MarketOrderModel>("dbo.[spMarketOrderPriceStillValid]", marketOrder, "StockExchangeData");
            var result = output.First();
            return result == 1;
        }
    }


}