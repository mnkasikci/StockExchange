using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.EndPoints
{
    public class ItemsEndpoint : IItemsEndPoint
    {
        private readonly IAuthorizedApiHelper _helper;

        public ItemsEndpoint(IAuthorizedApiHelper helper)
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
        public async Task<List<PendingItemShowModel>> GetAllPendingItems()
        {
            using (HttpResponseMessage response = await _helper.Client.GetAsync("/api/Items/Pending"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<PendingItemShowModel>>();
                    return result;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }

        }
    }
}
