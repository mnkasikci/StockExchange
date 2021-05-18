using StockExchangeDesktopUI.Library.Models;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.EndPoints
{
    public interface IUserEndPoint
    {
        Task<LoggedInUserModel> GetLoggedInUserInfo(string token);
    }
}