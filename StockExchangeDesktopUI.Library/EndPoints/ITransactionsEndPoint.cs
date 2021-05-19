using StockExchangeDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.EndPoints
{
    public interface ITransactionsEndPoint
    {
        Task<List<TransactionModel>> GetAllTransactions();
        Task<List<TransactionModel>> GetTransactionsByID();
    }
}