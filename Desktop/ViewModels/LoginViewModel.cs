using Caliburn.Micro;
using System;
using StockExchangeDesktopUI.Library.Api;
using Desktop.Models;
using Desktop.ViewModelInterfaces;
using StockExchangeDesktopUI.Library.Models;
using StockExchangeDesktopUI.Library.EndPoints;
using System.Threading.Tasks;
using System.Threading;

namespace Desktop.ViewModels
{
    public class LoginViewModel : Screen, IHasSensitiveInfo
    {
        private IAnonymousApiHelper _apihelper;
        private IEventAggregator _eventAggregator;
        private string _errorMessage;
        private string _password;
        private string _userName;
        private ILoggedInUserModel _loggedInUser;
        private bool _attemptingLogin = false;
        private readonly IUserEndPoint _userEndPoint;

        public LoginViewModel(IAnonymousApiHelper aPIHelper, IEventAggregator eventAggregator, ILoggedInUserModel loggedInUser, IUserEndPoint userEndPoint)
        {
            _apihelper = aPIHelper;
            _eventAggregator = eventAggregator;
            _loggedInUser = loggedInUser;
            _userEndPoint = userEndPoint;
        }

        public bool CanLoginButton => UserName?.Length > 0 && Password?.Length > 0 && !_attemptingLogin;

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
                _attemptingLogin = true;
                var result = await _apihelper.Authenticate(UserName, Password);
                StatusMessage = "Login successful! Redirecting to your account..";
                _loggedInUser.GetData(await _userEndPoint.GetLoggedInUserInfo(result.Access_Token));

                await _eventAggregator.PublishOnUIThreadAsync(new LogOnEvent());
                _attemptingLogin = false;

            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                _attemptingLogin = false;
            }
            
        }
        public async void SignupButton()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new UserWantsToRegisterEvent());
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
        }
    }
}
