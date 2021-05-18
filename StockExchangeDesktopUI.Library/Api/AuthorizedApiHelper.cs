using StockExchangeDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.Api
{
    public class AuthorizedApiHelper : IAuthorizedApiHelper
    {
        private string _token;
        private string _userID;
        private HttpClient _client;

        public string UserID { get => _userID;}
        public string Token { get => _token; }
        public HttpClient Client { get => _client; }

        public void SetApi(string token, string userID)
        {
            _token = token;
            _userID = userID;

            _client = new HttpClient();
            string apiPath = ConfigurationManager.AppSettings["apipath"];
            _client.BaseAddress = new Uri(apiPath);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
        }


        
    }
}
