using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchangeDesktopUI.Library.Models
{
    public class UserRegistrationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string TCIDNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
