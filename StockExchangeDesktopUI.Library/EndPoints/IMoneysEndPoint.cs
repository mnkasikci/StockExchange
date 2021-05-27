using StockExchangeDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.EndPoints
{
    public interface IMoneysEndPoint
    {
        Task AddPendingMoney(AddPendingMoneyModel moneyModel);
        Task AuthorizePendingMoney(PendingMoneyModel PendingMoneyID);
        Task CreateBuyOffer(OfferModel offer);
        Task<List<CurrencyType>> GetAllCurrencyTypes();
        Task<List<PendingMoneyModel>> GetAllPendingMoneys();
        Task<decimal> GetUserMoneyByID();
        Task RefusePendingMoney(PendingMoneyModel PendingMoneyID);
    }
}