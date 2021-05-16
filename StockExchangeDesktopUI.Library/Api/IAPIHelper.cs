using StockExchangeDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string userName, string password);
        Task<List<ItemTypeModel>> GetItemTypesInfo(string token);
        Task UpdateLoggedInUserInfo(string token);
    }
}