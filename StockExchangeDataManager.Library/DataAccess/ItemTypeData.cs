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
        public static List<ItemTypeModel> GetItemTypes()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new {};

            var output = sql.LoadData<ItemTypeModel, dynamic>("dbo.spGetItemTypes", p, "StockExchangeData");

            return output;
        }
    }
}
