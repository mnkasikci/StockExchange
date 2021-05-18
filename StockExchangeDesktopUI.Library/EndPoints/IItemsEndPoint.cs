using StockExchangeDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.EndPoints
{
    public interface IItemsEndPoint
    {
        Task AddPendingItem(AddPendingItemModel apim);
        Task AuthorizePendingItem(PendingItemModel pendingItem);
        Task<List<PendingItemModel>> GetAllPendingItems();
        Task<List<ItemTypeModel>> GetItemTypesInfo();
        
        Task<List<UserItemModel>> GetUserItems();
        Task RefusePendingItem(PendingItemModel PendingItemID);
    }
}