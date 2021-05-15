using Caliburn.Micro;
using StockExchangeUserInterface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.ViewModels
{
    public class ShellViewModel :Conductor<object>
    {
        LoginViewModel _loginVM;

        public ShellViewModel(LoginViewModel loginVM)
        {
            _loginVM = loginVM;
            ActivateItemAsync(_loginVM);
        }
    }
}
