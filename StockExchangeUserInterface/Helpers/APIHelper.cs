using StockExchangeUserInterface.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeUserInterface.Helpers
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient _apiClient { get; set; }

        public APIHelper()
        {
            InitializeClient();
        }
        private void InitializeClient()
        {
            string apiPath = ConfigurationManager.AppSettings["apipath"];

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = (new Uri(apiPath));
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<AuthenticatedUser> Authenticate(string userName, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string> ("grant_type","password"),
                new KeyValuePair<string,string> ("username",userName),
                new KeyValuePair<string,string> ("password",password)

            });

            using (HttpResponseMessage response = await _apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<AuthenticatedUser>();
                else
                    throw new Exception(response.ReasonPhrase);

            }

        }

    }
}
