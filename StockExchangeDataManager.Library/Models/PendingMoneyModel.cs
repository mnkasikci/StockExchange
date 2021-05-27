using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchangeDataManager.Library.Models
{
    public class PendingMoneyModel
    {
        public int PendingId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

