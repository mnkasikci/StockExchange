using Microsoft.AspNet.Identity;
using StockExchangeDataManager.Library.DataAccess;
using StockExchangeDataManager.Library.Models;
using System.Web.Http;


namespace StockExchangeDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
               
        // GET: User/Details/5
        public UserModel GetByID()
        {
            string userID = RequestContext.Principal.Identity.GetUserId();

            UserData data = new UserData();
            return data.GetUserById(userID);
        }

    }
}
