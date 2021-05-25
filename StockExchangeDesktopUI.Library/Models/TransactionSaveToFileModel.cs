using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchangeDesktopUI.Library.Models
{
    public class TransactionSaveToFileModel
    {
        public int TransactionID { get; set; }
        public DateTime LocalTransactionDate { get; set; }
        public string BuyerFullName { get; set; }
        public string SellerFullName { get; set; }
        public string ItemTypeName { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Amount * UnitPrice;
        public TransactionSaveToFileModel(TransactionModel tocopy)
        {
            TransactionID = tocopy.TransactionID;
            LocalTransactionDate = tocopy.LocalTransactionDate;
            BuyerFullName = tocopy.BuyerFullName;
            SellerFullName = tocopy.SellerFullName;
            ItemTypeName = tocopy.ItemTypeName;
            Amount = tocopy.Amount;
            UnitPrice = tocopy.UnitPrice;
        }

    }
}
