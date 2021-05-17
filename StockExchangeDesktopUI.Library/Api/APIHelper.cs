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
        


        public APIHelper(ILoggedInUserModel loggedInUser)
        {
            InitializeClient();
        }
        private void InitializeClient()
        {
            string apiPath = ConfigurationManager.AppSettings["apipath"];

            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(apiPath);

        }
        public async Task<AuthenticatedUser> Authenticate(string userName, string password)
        {
            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
        public async Task<LoggedInUserModel> GetLoggedInUserInfo(string token)
        {

            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");


            using (HttpResponseMessage response = await apiClient.GetAsync("/api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<LoggedInUserModel>();
                    
                }
                else
                    throw new Exception(response.ReasonPhrase);

            }
        }
        public async Task RegisterUser(UserRegistrationModel urm)
        {

            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));




            using (HttpResponseMessage response = await apiClient.PostAsJsonAsync<UserRegistrationModel>("/api/User/Register", urm))
            {
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                else
                    throw new Exception(response.ReasonPhrase.ToString());

            }
        }



        public async Task<List<ItemTypeModel>> GetItemTypesInfo(string token)
        {
            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            using (HttpResponseMessage response = await apiClient.GetAsync("/api/Items"))
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

    }
}
