using StockExchangeDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.EndPoints
{
    public interface IItemsEndPoint
    {
        Task AddNewItemType(string ItemTypeName);
        Task AddPendingItem(AddPendingItemModel apim);
        Task AuthorizePendingItem(PendingItemModel pendingItem);
        Task CreateSellOffer(OfferModel offer);
        Task<List<PendingItemModel>> GetAllPendingItems();
        Task<List<ItemTypeModel>> GetItemTypesInfo();
        Task<List<GetSellOffersModel>> GetSellOffersByID(int ItemTypeId);
        Task<List<UserItemModel>> GetUserItems();
        Task IssueMarketOrder(MarketOrderModel offer);
        Task RefusePendingItem(PendingItemModel PendingItemID);
    }
}