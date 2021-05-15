using StockExchangeDesktopUI.Library.Models;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string userName, string password);
        Task GetLoggedInUserInfo(string token);
    }
}