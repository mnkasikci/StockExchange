using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchangeDataManager.Library.Models
{
    public class PendingItemShowModel
    {
        public int PendingId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ItemTypeName { get; set; }
        public int Amount { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
