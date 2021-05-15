using StockExchangeUserInterface.Models;
using System.Threading.Tasks;

namespace StockExchangeUserInterface.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string userName, string password);
    }
}