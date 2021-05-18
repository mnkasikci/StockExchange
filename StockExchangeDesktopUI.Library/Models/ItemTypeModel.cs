using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.Models
{

    public class ItemTypeListModel : IItemTypeListModel
    {
        private List<ItemTypeModel> _itemTypeList;

        public List<ItemTypeModel> ItemTypeList
        {
            get { return _itemTypeList; }
            set { _itemTypeList = value; }
        }

    }

    public class ItemTypeModel
    {
        public int ItemTypeID { get; set; }
        public string ItemTypeName { get; set; }
    }

}
