using Microsoft.AspNet.Identity;
using StockExchangeDataManager.Library.DataAccess;
using StockExchangeDataManager.Library.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace StockExchangeDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/Items")]
    public class ItemController : ApiController
    {
        public List<ItemTypeModel> GetItemTypes()
        {
            return ItemTypeData.GetItemTypes();
        }
    }
}
