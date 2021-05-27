using Microsoft.Extensions.Configuration;
using StockExchangeDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Xml;

namespace StockExchangeDataManager.Library.Helpers
{
    public class CurrencyHelper
    {

        private readonly IConfiguration _config;
        private readonly string _TCMBXmlLink;
        Dictionary<string, decimal> CurrencyExchangeRates = new();
        public CurrencyHelper(IConfiguration config)
        {
            _config = config;
            _TCMBXmlLink = _config.GetConnectionString("TCMBExchangeRates");
            GetCurrencyRatesToTurkishLira();
        }
        public decimal GetCurrencyRateToTurkishLira(string currencyCode)
        {
            if (currencyCode == "TRY")
                return 1;

            if (CurrencyExchangeRates.ContainsKey(currencyCode))
                    return CurrencyExchangeRates[currencyCode];
            else
                throw new DataException(currencyCode + " doesn't exist or couldn't load from " + _TCMBXmlLink);
            
        }
        private void GetCurrencyRatesToTurkishLira()
        {
            XmlDocument doc = new();

            using (XmlReader xml = XmlReader.Create(_TCMBXmlLink))
                doc.Load(xml);
            XmlNodeList currencyList = doc.SelectNodes("/Tarih_Date/Currency");

            for (int i = 0; i < currencyList.Count; i++)
            {
                string currencyCode = currencyList[i].SelectSingleNode("@CurrencyCode").InnerText;

                decimal shownExchangeRate;
                int unit;

                bool exchangeRateSuccess = decimal.TryParse(
                    currencyList[i].SelectSingleNode("ForexBuying").InnerText,
                    NumberStyles.AllowDecimalPoint,
                    CultureInfo.InvariantCulture,
                    out shownExchangeRate);
                    
                    
                    
                bool unitSuccess = int.TryParse(currencyList[i].SelectSingleNode("Unit").InnerText, out unit);

                if (!exchangeRateSuccess || !unitSuccess) continue;

                decimal actualExchangeRate = shownExchangeRate / unit;


                CurrencyExchangeRates.Add(currencyCode, actualExchangeRate);

            }
        }
    }
}



