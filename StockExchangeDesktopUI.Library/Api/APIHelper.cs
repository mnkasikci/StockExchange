using StockExchangeDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.Api
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient apiClient { get; set; }
        private ILoggedInUserModel _loggedInUser;


        public APIHelper(ILoggedInUserModel loggedInUser)
        {
            InitializeClient();
            _loggedInUser = loggedInUser;
        }
        private void InitializeClient()
        {
            string apiPath = ConfigurationManager.AppSettings["apipath"];

            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(apiPath);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<AuthenticatedUser> Authenticate(string userName, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string> ("grant_type","password"),
                new KeyValuePair<string,string> ("username",userName),
                new KeyValuePair<string,string> ("password",password)

            });

            using (HttpResponseMessage response = await apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<AuthenticatedUser>();
                else
                    throw new Exception(response.ReasonPhrase);

            }
        }
        public async Task GetLoggedInUserInfo(string token)
        {

            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");


            using (HttpResponseMessage response = await apiClient.GetAsync("/api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                    _loggedInUser.GetData(result,token);
                }
                else
                    throw new Exception(response.ReasonPhrase);

            }
        }

    }
}
