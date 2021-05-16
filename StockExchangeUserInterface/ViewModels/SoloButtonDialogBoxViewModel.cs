
using Caliburn.Micro;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows;

namespace StockExchangeUserInterface.ViewModels
{
    public class SoloButtonDialogBoxViewModel : Screen
    {

        public SoloButtonDialogBoxViewModel(IWindowManager wm)
        {
            _wm = wm;
        }
        private string _messageBody;

        public string MessageBody
        {
            get { return _messageBody; }
            private set { _messageBody = value; }
        }

        private string _buttonContent;
        private IWindowManager _wm;

        public string ButtonContent
        {
            get { return _buttonContent; }
            private set { _buttonContent = value; }
        }

        internal Task SetAndShow(string header, string content, string buttontext)
        {

            MessageBody = content;
            ButtonContent = buttontext;

            NotifyOfPropertyChange(() => MessageBody);
            NotifyOfPropertyChange(() => ButtonContent);

            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Title = header;

            return _wm.ShowDialogAsync(this, null, settings);

        }
        public async void Button()
        {
            await TryCloseAsync();
        }
    }

}
