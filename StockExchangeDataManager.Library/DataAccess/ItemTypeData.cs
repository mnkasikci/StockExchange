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
    public class ItemTypeData
    {
        private readonly IConfiguration _config;

        public ItemTypeData(IConfiguration config)
        {
            _config = config;
        }
        public async Task<List<ItemTypeModel>> GetItemTypes()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new {};

            var output = await sql.LoadDataAsync<ItemTypeModel, dynamic>("dbo.spGetItemTypes", p, "StockExchangeData");

            return output;
        }

        public async Task AddItemToAuthorize(AddPendingItemModel itemwa,string UserID)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { 
                @UserID = UserID,
                @ItemTypeID = itemwa.ItemTypeID,
                @Amount = itemwa.Amount
            };

            await sql.SaveData<dynamic>("dbo.[spAddItemToAuthorize]", p, "StockExchangeData");
        }

        public async Task<List<PendingItemModel>> GetAllPendingItems()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { };
            var output =await sql.LoadDataAsync<PendingItemModel, dynamic>("dbo.spGetAllPendingItems", p, "StockExchangeData");
            return output;

        }

        public async Task AuthorizePendingItem(int pendingItemID, string userID)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new {
                PendingItemID = pendingItemID,
                AuthorizerID =userID
            };
            await sql.SaveData<dynamic>("dbo.[spAuthorizePendingItem]", p, "StockExchangeData");
        }

        public async Task RefusePendingItem(int pendingId, string userID)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new
            {
                PendingItemID = pendingId,
                RefuserID = userID
            };
            await sql.SaveData<dynamic>("dbo.[spRefusePendingItem]", p, "StockExchangeData");
        }

        public async Task<List<UserItemModel>> GetUserItems(string userID)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { userID = userID };

            var output = await sql.LoadDataAsync<UserItemModel, dynamic>("dbo.spGetUserItemsByID", p, "StockExchangeData");

            return output;
        }

        public async Task CreateSellOffer(OfferModel offer)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            await sql.SaveData<dynamic>("dbo.[spCreateSellOffer]", offer, "StockExchangeData");
        }
        public async Task AddNewItemType(string ItemTypeName)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new
            {
                ItemTypeName = ItemTypeName
            };
            await sql.SaveData<dynamic>("dbo.[spAddNewItemType]", p, "StockExchangeData");
        }

        public async Task<List<GetSellOffersModel>> GetSellOffersByItemId(int ItemTypeID)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { ItemTypeID = ItemTypeID };

            var output = await sql.LoadDataAsync<GetSellOffersModel, dynamic>("dbo.spGetSellOffersByItemTypeId", p, "StockExchangeData");

            return output;
        }

        public async Task IssueMarketOrder(MarketOrderModel offer)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            await sql.SaveData<dynamic>("dbo.[spTryMarketOrder]", offer, "StockExchangeData");
        }


    }
}
