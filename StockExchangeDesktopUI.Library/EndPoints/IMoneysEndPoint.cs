using StockExchangeDesktopUI.Library.Models;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.EndPoints
{
    public interface IMoneysEndPoint
    {
        Task AddPendingMoney(AddPendingMoneyModel moneyModel);
        Task<decimal> GetUserMoneyByID();
    }
}