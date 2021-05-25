namespace StockExchangeDesktopUI.Library.Models
{
    public class MarketOrderModel
    {
        public string UserId { get; set; }
        public int ItemTypeID { get; set; }
        public int ItemAmount { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
