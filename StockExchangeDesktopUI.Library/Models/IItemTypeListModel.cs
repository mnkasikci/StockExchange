using System.Collections.Generic;

namespace StockExchangeDesktopUI.Library.Models
{
    public interface IItemTypeListModel
    {
        List<ItemTypeModel> ItemTypeList { get; set; }
    }
}