using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchangeDesktopUI.Library.Models
{
    public class OfferModel
    {
        public int ItemIndexID { get; set; }
        public int Amount { get; set; }
        public double UnitPrice { get; set; }
    }
}