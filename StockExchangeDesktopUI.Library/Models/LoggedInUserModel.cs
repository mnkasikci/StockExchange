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
        public bool IsAdmin { get; set; }

        public void GetData(LoggedInUserModel modelToCopy)
        {
            this.ID = modelToCopy.ID;
            this.FirstName = modelToCopy.FirstName;
            this.LastName = modelToCopy.LastName;
            this.UserName = modelToCopy.UserName;
            this.TCIDNumber = modelToCopy.TCIDNumber;
            this.PhoneNumber = modelToCopy.PhoneNumber;
            this.EmailAddress = modelToCopy.EmailAddress;
            this.Address = modelToCopy.Address;
            this.Token = modelToCopy.Token;
            this.IsAdmin = modelToCopy.IsAdmin;
        }
    }
}
