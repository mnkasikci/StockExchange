using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchangeDesktopUI.Library.Models
{
    public class PendingItemShowModel
    {
        public int PendingId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ItemTypeName { get; set; }
        public int Amount { get; set; }
        public DateTime CreationDate { get => _creationDate.ToLocalTime(); set => _creationDate = value; }
        private DateTime _creationDate;
    }
}
