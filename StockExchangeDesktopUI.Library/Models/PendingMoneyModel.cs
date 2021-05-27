using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchangeDesktopUI.Library.Models
{
    public class PendingMoneyModel
    {
        public int PendingId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public int Amount { get; set; }
        public DateTime CreationLocalDate => CreationDate.ToLocalTime();
        public DateTime CreationDate { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
    }
}
