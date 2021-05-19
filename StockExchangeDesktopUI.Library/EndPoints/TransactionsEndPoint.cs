using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.EndPoints
{
    public class TransactionsEndPoint : ITransactionsEndPoint
    {
        private readonly IAuthorizedApiHelper _helper;

        public TransactionsEndPoint(IAuthorizedApiHelper helper)
        {
            _helper = helper;
        }
        public async Task<List<TransactionModel>> GetTransactionsByID()
        {
            using (HttpResponseMessage response = await _helper.Client.GetAsync("/api/Transactions"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<TransactionModel>>();
                    return result;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }
        }
        public async Task<List<TransactionModel>> GetAllTransactions()
        {
            using (HttpResponseMessage response = await _helper.Client.GetAsync("/api/Transactions/Admin"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<TransactionModel>>();
                    return result;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }

        }

    }
}
