using StockExchangeDataManager.Library.Internal.DataAccess;
using StockExchangeDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeDataManager.Library.DataAccess
{
    public class UserData
    {
        public UserModel GetUesrById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id = Id };

            var output = sql.LoadData<UserModel, dynamic>("dbo.spGetUserData", p, "StockExchangeData").First();

            return output;
        } 
    }
}
