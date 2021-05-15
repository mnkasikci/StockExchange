using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.Models
{
    public interface ILoggedInUserModel
    {
        string Address { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string ID { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string TCIDNumber { get; set; }
        string Token { get; set; }
        string UserName { get; set; }
        void GetData(LoggedInUserModel result,string token);
    }
}