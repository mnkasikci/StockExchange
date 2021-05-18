using StockExchangeDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.EndPoints
{
    public interface IItemsEndPoint
    {
        Task AddPendingItem(AddPendingItemModel apim);
        Task<List<PendingItemShowModel>> GetAllPendingItems();
        Task<List<ItemTypeModel>> GetItemTypesInfo();
    }
}