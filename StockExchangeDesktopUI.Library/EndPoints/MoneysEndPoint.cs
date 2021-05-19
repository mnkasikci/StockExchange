using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
                    var result = await response.Content.ReadAsAsync<decimal>();
                    return result;
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
        public async Task AuthorizePendingMoney(PendingMoneyModel PendingMoneyID)
        {

            using (HttpResponseMessage response = await _helper.Client.PostAsJsonAsync("/api/Moneys", PendingMoneyID))
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
                    var result = await response.Content.ReadAsAsync<List<PendingMoneyModel>>();
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

    }
}