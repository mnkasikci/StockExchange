using Microsoft.Extensions.Configuration;
using StockExchangeDataManager.Library.Internal.DataAccess;
using StockExchangeDataManager.Library.Internal.Models;
using StockExchangeDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeDataManager.Library.DataAccess
{
    public class UserSqlData
    {
        private readonly IConfiguration _config;

        public UserSqlData(IConfiguration config)
        {
            _config = config;
        }
        public async Task<SqlUserModel> GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { Id = Id };

            var OutputList =await sql.LoadData<SqlUserModel, dynamic>("dbo.spGetUserDataByID", p, "StockExchangeData");

            return OutputList.First();
        }
        public async Task<bool> TCIDNumberNotExist(string TCNumber)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            var p = new { TC = TCNumber };
            var OutputList = await sql.LoadData<SqlUserModel, dynamic>("dbo.spGetUserDataByTC", p, "StockExchangeData");

            return OutputList.Count == 0;
        }

        public async Task SaveUserToSqlDB(SqlUserModel sum)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);
            
            await sql.SaveData<SqlUserModel>("dbo.spSaveUser", sum, "StockExchangeData");

        }
    }
}
