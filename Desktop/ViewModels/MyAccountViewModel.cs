using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Models;
using Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class MyAccountViewModel: Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggedInUserModel _loggedInUserModel;
        private string address;

        private string emailAddre;

        private string firstName;

        private string lastName;

        private string phoneNumber;

        private string tCIDNumber;

        private string userName;

        public MyAccountViewModel(ILoggedInUserModel loggedInUserModel, IEventAggregator eventAggregator)
        {
            _loggedInUserModel = loggedInUserModel;
            _eventAggregator = eventAggregator;
        }

        public string Address { get => address; set { address = value; NotifyOfPropertyChange(() => Address); } }

        public string EmailAddress { get => emailAddre; set { emailAddre = value; NotifyOfPropertyChange(() => EmailAddress); } }

        public string FirstName { get => firstName; set { firstName = value; NotifyOfPropertyChange(() => FirstName); } }

        public string LastName { get => lastName; set { lastName = value; NotifyOfPropertyChange(() => LastName); } }

        public string PhoneNumber { get => phoneNumber; set { phoneNumber = value; NotifyOfPropertyChange(() => PhoneNumber); } }

        public string TCIDNumber { get => tCIDNumber; set { tCIDNumber = value; NotifyOfPropertyChange(() => TCIDNumber); } }

        public string UserName { get => userName; set { userName = value; NotifyOfPropertyChange(() => UserName); } }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
            if(_loggedInUserModel== null || _loggedInUserModel.ID==null)
            {
                await _eventAggregator.PublishOnUIThreadAsync(new PreviousButtonClickedEvent());
                await TryCloseAsync();
            }
            Address = _loggedInUserModel.Address;
            EmailAddress = _loggedInUserModel.EmailAddress;
            FirstName = _loggedInUserModel.FirstName;
            LastName = _loggedInUserModel.LastName;
            PhoneNumber = _loggedInUserModel.PhoneNumber;
            TCIDNumber = _loggedInUserModel.TCIDNumber;
            UserName = _loggedInUserModel.UserName;
        }
    }
}
