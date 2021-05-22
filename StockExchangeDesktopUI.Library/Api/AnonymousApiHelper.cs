using StockExchangeDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.Api
{
    public class AnonymousApiHelper : IAnonymousApiHelper
    {
        private readonly IAuthorizedApiHelper _authorizedApiHelper;

        private HttpClient apiClient;

        public AnonymousApiHelper(IAuthorizedApiHelper authorizedApiHelper)
        {
            _authorizedApiHelper = authorizedApiHelper;
            InitializeClient();
        }
        private void InitializeClient()
        {
            string apiPath = ConfigurationManager.AppSettings["apipath"];
            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(apiPath);

        }
        public async Task<AuthenticatedUser> Authenticate(string userNameorEmail, string password)
        {
            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string> ("grant_type","password"),
                new KeyValuePair<string,string> ("userNameorEmail",userNameorEmail),
                new KeyValuePair<string,string> ("password",password)

            });

            using (HttpResponseMessage response = await apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    AuthenticatedUser authenticatedUser = await response.Content.ReadFromJsonAsync<AuthenticatedUser>();
                    _authorizedApiHelper.SetApi(authenticatedUser.Access_Token, authenticatedUser.UserID);
                    return authenticatedUser;
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
            SetForAuthorizedAction(token);
            using (HttpResponseMessage response = await apiClient.GetAsync("/api/Items"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<ItemTypeModel>>();
                    return result;
                }
                else
                    throw new Exception(response.ReasonPhrase);

            }

        }

        private void SetForAuthorizedAction(string token)
        {
            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
    }
}
