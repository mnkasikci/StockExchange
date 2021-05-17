using StockExchangeDataManager.Library.Internal.Models;

namespace StockExchangeDataManager.Library.Models
{
    public class UserDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string TCIDNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool IsAdmin { get; set;}
        public string Address { get; set; }
        public string ID { get; set; }
        public UserDataModel(SqlUserModel sum, string userName, string emailAddress,bool isAdmin)
        {
            FirstName = sum.FirstName;
            LastName = sum.LastName;
            UserName = userName;
            TCIDNumber = sum.TCIDNumber;
            PhoneNumber = sum.PhoneNumber;
            EmailAddress = emailAddress;
            IsAdmin = isAdmin;
            Address = sum.Address;
            ID = sum.ID;

        }
        public UserDataModel(UserRegistrationModel urm,string iD)
        {
            FirstName = urm.FirstName;
            LastName = urm.LastName;
            UserName = urm.UserName;
            TCIDNumber = urm.TCIDNumber;
            PhoneNumber = urm.PhoneNumber;
            EmailAddress = urm.EmailAddress;
            Address = urm.Address;
            ID = iD;
        }
    }
}