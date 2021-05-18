using System.Net.Http;

namespace StockExchangeDesktopUI.Library.Api
{
    public interface IAuthorizedApiHelper
    {
        void SetApi(string token, string userID);
        string UserID { get; }
        string Token { get; }
        HttpClient Client { get ; }
    }
}