using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchangeDataManager.Library.Models
{
    public class AddPendingMoneyModel
    {
        public string UserID { get; set; }
        public decimal Amount { get; set; }
    }
}
