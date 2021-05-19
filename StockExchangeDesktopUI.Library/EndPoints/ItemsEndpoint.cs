using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.EndPoints
{
    public class ItemsEndPoint : IItemsEndPoint
    {
        private readonly IAuthorizedApiHelper _helper;

        public ItemsEndPoint(IAuthorizedApiHelper helper)
        {
            _helper = helper;
        }

        public async Task<List<ItemTypeModel>> GetItemTypesInfo()
        {
            using (HttpResponseMessage response = await _helper.Client.GetAsync("/api/Items"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ItemTypeModel>>();
                    return result;
                }
                else
                    throw new Exception(response.ReasonPhrase);

            }
        }
        public async Task AddPendingItem(AddPendingItemModel apim)
        {
            using (HttpResponseMessage response = await _helper.Client.PostAsJsonAsync<AddPendingItemModel>("/api/Items/Pending", apim))
            {
                if (response.IsSuccessStatusCode)
                    return;
                else
                    throw new Exception(response.ReasonPhrase);

            }
        }
        public async Task<List<PendingItemModel>> GetAllPendingItems()
        {
            using (HttpResponseMessage response = await _helper.Client.GetAsync("/api/Items/Pending"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<PendingItemModel>>();
                    return result;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }

        }
        public async Task AuthorizePendingItem(PendingItemModel PendingItemID)
        {

            using (HttpResponseMessage response = await _helper.Client.PostAsJsonAsync("/api/Items", PendingItemID))
            {
                if (response.IsSuccessStatusCode)
                    return;
                else
                    throw new Exception(response.ReasonPhrase);

            }
        }
        public async Task RefusePendingItem(PendingItemModel PendingItemID)
        {

            using (HttpResponseMessage response = await _helper.Client.PostAsJsonAsync("/api/Items/Refuse", PendingItemID))
            {
                if (response.IsSuccessStatusCode)
                    return;
                else
                    throw new Exception(response.ReasonPhrase);

            }
        }
        public async Task<List<UserItemModel>> GetUserItems()
        {
            using (HttpResponseMessage response = await _helper.Client.GetAsync("/api/Items/Inventory"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<UserItemModel>>();
                    return result;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }

        }
        public async Task CreateSellOffer(OfferModel offer)
        {
            using (HttpResponseMessage response= await _helper.Client.PostAsJsonAsync("/api/Items/SellOffers",offer))
            {
                if (response.IsSuccessStatusCode)
                    return;
                else
                    throw new Exception(response.ReasonPhrase);
            }
        }



    }
}
