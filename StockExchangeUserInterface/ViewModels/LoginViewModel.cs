using Caliburn.Micro;
using StockExchangeUserInterface.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeUserInterface.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private IAPIHelper _apihelper;


        public LoginViewModel(IAPIHelper aPIHelper)
        {
            _apihelper = aPIHelper;
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
        private string _password;

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

        public async void LoginButton()
        {
            try
            {
                var result = await _apihelper.Authenticate(UserName, Password);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            

            
        }
        public bool CanLoginButton =>  UserName?.Length > 0 && Password?.Length > 0;


    }
}
