using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchangeDesktopUI.Library.Models
{
    public class OfferModel
    {
        public string OffererID { get; set; }
        public int ItemTypeID { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
