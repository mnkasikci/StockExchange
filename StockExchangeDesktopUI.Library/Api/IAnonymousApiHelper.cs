using StockExchangeDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.Api
{
    public interface IAnonymousApiHelper
    {
        Task<AuthenticatedUser> Authenticate(string userName, string password);
        Task RegisterUser(UserRegistrationModel urm);
    }
}