using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using StockExchangeUserInterface.Models;
using StockExchangeUserInterface.ViewModelInterfaces;
using System;

namespace StockExchangeUserInterface.ViewModels
{
    public class RegisterUserViewModel : Screen, IHasSensitiveInfo
    {
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _tCIDNumber;
        private string _phoneNumber;
        private string _emailAddress;
        private string _address;
        private string _password;
        private string _confirmPassword;
        private string _statusMessage;
        private readonly IAnonymousApiHelper _apiHelper;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggedInUserModel _loggedInUserModel;
        private readonly IUserEndPoint _userEnd;

        public bool CanRegisterButton
        {
            get
            {
                return FirstName?.Length > 0
                    && LastName?.Length > 0
                    && UserName?.Length > 0
                    && TCIDNumber?.Length > 0
                    && PhoneNumber?.Length > 0
                    && EmailAddress?.Length > 0
                    && Address?.Length > 0
                    && Password?.Length > 0
                    && ConfirmPassword?.Length > 0
                    && IsConfirmPasswordOK;
            }
        }
        public bool IsConfirmPasswordOK
        {
            get
            {
                if (Password == null || ConfirmPassword == null)
                    return false;
                return Password == ConfirmPassword;

            }
        }

        public RegisterUserViewModel(IAnonymousApiHelper aPIHelper, IEventAggregator eventAggregator,ILoggedInUserModel loggedInUserModel, IUserEndPoint userEnd)
        {
            _apiHelper = aPIHelper;
            _eventAggregator = eventAggregator;
            _loggedInUserModel = loggedInUserModel;
            _userEnd = userEnd;
        }

        public async void RegisterButton()
        {
            try
            {
                UserRegistrationModel urm = new UserRegistrationModel
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    UserName = UserName,
                    TCIDNumber = TCIDNumber,
                    PhoneNumber = PhoneNumber,
                    EmailAddress = EmailAddress,
                    Address = Address,
                    Password = Password
                };
                await _apiHelper.RegisterUser(urm);
                StatusMessage = ("Your account has been created. You are being redirected...");


                var tempAuthenticatedUser = await _apiHelper.Authenticate(urm.UserName, urm.Password);
                _loggedInUserModel.GetData(await _userEnd.GetLoggedInUserInfo(tempAuthenticatedUser.Access_Token));

                await _eventAggregator.PublishOnUIThreadAsync(new LogOnEvent());

            }

            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }

        }

        public async void BackButton()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new LoadLoginScreenEvent());
        }

        public string FirstName { get => _firstName; set { _firstName = value; NotifyOfPropertyChange(() => CanRegisterButton); } }
        public string LastName { get => _lastName; set { _lastName = value; NotifyOfPropertyChange(() => CanRegisterButton); } }
        public string UserName { get => _userName; set { _userName = value; NotifyOfPropertyChange(() => CanRegisterButton); } }
        public string TCIDNumber { get => _tCIDNumber; set { _tCIDNumber = value; NotifyOfPropertyChange(() => CanRegisterButton); } }
        public string PhoneNumber { get => _phoneNumber; set { _phoneNumber = value; NotifyOfPropertyChange(() => CanRegisterButton); } }
        public string EmailAddress { get => _emailAddress; set { _emailAddress = value; NotifyOfPropertyChange(() => CanRegisterButton); } }
        public string Address { get => _address; set { _address = value; NotifyOfPropertyChange(() => CanRegisterButton); } }
        public string Password { get => _password; set { _password = value; NotifyOfPropertyChange(() => CanRegisterButton); } }
        public string ConfirmPassword { get => _confirmPassword; set { _confirmPassword = value; NotifyOfPropertyChange(() => CanRegisterButton); NotifyOfPropertyChange(() => IsConfirmPasswordOK); } }

        public string StatusMessage { get => _statusMessage; set { _statusMessage = value; NotifyOfPropertyChange(() => StatusMessage); } }
    }
}
