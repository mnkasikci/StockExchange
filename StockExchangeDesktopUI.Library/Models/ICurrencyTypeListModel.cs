using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.Models
{
    public interface ICurrencyTypeListModel
    {
        List<CurrencyType> Currencies {get; set;}
    }
}
