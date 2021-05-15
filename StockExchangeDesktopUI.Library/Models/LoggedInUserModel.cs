using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {
        public string Token { get; set; }
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string TCIDNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }

        public void GetData(LoggedInUserModel result,string token)
        {
            this.ID = result.ID;
            this.FirstName = result.FirstName;
            this.LastName = result.LastName;
            this.UserName = result.UserName;
            this.TCIDNumber = result.UserName;
            this.PhoneNumber = result.PhoneNumber;
            this.EmailAddress = result.EmailAddress;
            this.Address = result.Address;
            this.Token = token;
        }
    }
}
