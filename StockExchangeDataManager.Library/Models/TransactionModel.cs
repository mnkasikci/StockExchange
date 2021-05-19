using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchangeDataManager.Library.Models
{
    public class TransactionModel
    {
		public int TransactionID { get; set; }
		public DateTime TransactionDate { get; set; }
		public string BuyerID { get; set; }
		public string BuyerFirstName { get; set; }

		public string BuyerLastName { get; set; }
		public string SellerId { get; set; }
		public string SellerFirstName { get; set; }
		public string SellerLastName { get; set; }
		public string ItemTypeName { get; set; }
		public int Amount { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
