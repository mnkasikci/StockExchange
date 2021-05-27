using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.Models
{
    public class CurrencyType
    {
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
    }

    public class CurrencyTypeList : ICurrencyTypeListModel
    {
        private List<CurrencyType> _currencies;
        public List<CurrencyType> Currencies { get => _currencies; set => _currencies = value; }
    }
}
