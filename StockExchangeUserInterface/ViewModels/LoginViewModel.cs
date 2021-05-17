using Caliburn.Micro;
using System;
using StockExchangeDesktopUI.Library.Api;
using StockExchangeUserInterface.Models;
using StockExchangeUserInterface.ViewModelInterfaces;
using StockExchangeDesktopUI.Library.Models;

namespace StockExchangeUserInterface.ViewModels
{
    public class LoginViewModel : Screen, IHasSensitiveInfo
    {
        private IAPIHelper _apihelper;
        private IEventAggregator _eventAggregator;
        private string _errorMessage;
        private string _password;
        private string _userName;
        private ILoggedInUserModel _loggedInUser;
        public LoginViewModel(IAPIHelper aPIHelper, IEventAggregator eventAggregator, ILoggedInUserModel loggedInUser)
        {
            _apihelper = aPIHelper;
            _eventAggregator = eventAggregator;
            _loggedInUser = loggedInUser;
        }

        public bool CanLoginButton => UserName?.Length > 0 && Password?.Length > 0;

        public bool IsErrorVisible => StatusMessage?.Length > 0;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLoginButton);
            }
        }

        public string StatusMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => StatusMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);

            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLoginButton);
            }
        }
        public async void LoginButton()
        {
            try
            {
                var result = await _apihelper.Authenticate(UserName, Password);
                StatusMessage = "Login successful! Redirecting to your account..";
                _loggedInUser.GetData(await _apihelper.GetLoggedInUserInfo(result.Access_Token));

                await _eventAggregator.PublishOnUIThreadAsync(new LogOnEvent());

            }
            catch (Exception ex)
            {

                StatusMessage = ex.Message;
            }
            
        }
        public async void SignupButton()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new UserWantsToRegisterEvent());
        }
    }
}
