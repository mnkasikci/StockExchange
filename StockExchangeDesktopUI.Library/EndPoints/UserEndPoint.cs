using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.EndPoints
{
    public class UserEndPoint : IUserEndPoint
    {
        private readonly IAuthorizedApiHelper _helper;

        public UserEndPoint(IAuthorizedApiHelper helper)
        {
            _helper = helper;
        }
        public async Task<LoggedInUserModel> GetLoggedInUserInfo(string token)
        {

            using (HttpResponseMessage response = await _helper.Client.GetAsync("/api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var ret = await response.Content.ReadFromJsonAsync<LoggedInUserModel>();
                    ret.Token = token;
                    return ret;

                }
                else
                    throw new Exception(response.ReasonPhrase);

            }
        }
    }
}
