using StockExchangeDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeDataManager.Library.Internal.Models
{
    public class SqlUserModel
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TCIDNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public SqlUserModel() { }
        public SqlUserModel(UserDataModel udm)
        {
            ID = udm.ID;
            FirstName = udm.FirstName;
            LastName = udm.LastName;
            TCIDNumber = udm.TCIDNumber;
            PhoneNumber = udm.PhoneNumber;
            Address = udm.Address;
        }
    }
}
