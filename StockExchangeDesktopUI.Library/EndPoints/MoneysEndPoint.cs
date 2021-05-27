using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.EndPoints
{
    public class MoneysEndPoint : IMoneysEndPoint
    {
        private readonly IAuthorizedApiHelper _helper;

        public MoneysEndPoint(IAuthorizedApiHelper helper)
        {
            _helper = helper;
        }
        public async Task AddPendingMoney(AddPendingMoneyModel moneyModel)
        {
            using (HttpResponseMessage response = await _helper.Client.PostAsJsonAsync<AddPendingMoneyModel>("/api/Moneys/Pending", moneyModel))
            {
                if (response.IsSuccessStatusCode)
                    return;
                else
                    throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<decimal> GetUserMoneyByID()
        {
            using (HttpResponseMessage response = await _helper.Client.GetAsync("/api/Moneys"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<decimal>();
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }
        }
        public async Task RefusePendingMoney(PendingMoneyModel PendingMoneyID)
        {

            using (HttpResponseMessage response = await _helper.Client.PostAsJsonAsync("/api/Moneys/Refuse", PendingMoneyID))
            {
                if (response.IsSuccessStatusCode)
                    return;
                else
                    throw new Exception(response.ReasonPhrase);

            }
        }
        private record AuthorizePendingMoneyrecord(string pendingMoneyId);
        public async Task AuthorizePendingMoney(PendingMoneyModel PendingMoney)
        {

            AuthorizePendingMoneyrecord p = new(PendingMoney.PendingId.ToString());

            using (HttpResponseMessage response = await _helper.Client.PostAsJsonAsync("/api/Moneys", p))
            {
                if (response.IsSuccessStatusCode)
                    return;
                else
                    throw new Exception(response.ReasonPhrase);

            }
        }
        public async Task<List<PendingMoneyModel>> GetAllPendingMoneys()
        {
            using (HttpResponseMessage response = await _helper.Client.GetAsync("/api/Moneys/Pending"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<PendingMoneyModel>>();
                    return result;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }

        }
        public async Task CreateBuyOffer(OfferModel offer)
        {
            using (HttpResponseMessage response = await _helper.Client.PostAsJsonAsync("/api/Moneys/BuyOffers", offer))
            {
                if (response.IsSuccessStatusCode)
                    return;
                else
                    throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<List<CurrencyType>> GetAllCurrencyTypes()
        {
            using (HttpResponseMessage response = await _helper.Client.GetAsync("/api/Moneys/Currencies"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<CurrencyType>>();
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }
        }
    }
}