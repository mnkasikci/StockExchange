using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using Desktop.ViewModels;
using Desktop.Models;
using System;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.ViewModels
{
    public class AddMoneyViewModel : Screen
    {
        private readonly ICurrencyTypeListModel _currencyTypesList;

        private readonly IEventAggregator _eventAggregator;

        private readonly IMoneysEndPoint _moneysEndPoint;

        private readonly SoloButtonDialogBoxViewModel _soloDB;

        private decimal _amount;

        private BindableCollection<CurrencyType> _currencyTypes = new();
        private decimal _userMoneyAmount;

        public AddMoneyViewModel(IEventAggregator eventAggregator, IMoneysEndPoint moneysEndPoint, SoloButtonDialogBoxViewModel soloButtonDialogBox, ICurrencyTypeListModel currencyTypeListModel)
        {
            _eventAggregator = eventAggregator;
            _moneysEndPoint = moneysEndPoint;
            _soloDB = soloButtonDialogBox;
            _currencyTypesList = currencyTypeListModel;
        }
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                NotifyOfPropertyChange(() => Amount);
                NotifyOfPropertyChange(() => CanAddMoneyButton);
            }
        }

        public bool CanAddMoneyButton => Amount > 0 && SelectedCurrencyType!=null;

        public bool CanCreateOfferButton => UserMoneyAmount > 0;
        private CurrencyType _selectedCurrencyType;

        public CurrencyType SelectedCurrencyType
        {
            get { return _selectedCurrencyType; }
            set
            {
                _selectedCurrencyType = value; 
                NotifyOfPropertyChange(() => SelectedCurrencyType);
                NotifyOfPropertyChange(() => CanAddMoneyButton);
            }
        }

        public BindableCollection<CurrencyType> CurrencyTypes
        {
            get { return _currencyTypes; }
            set { _currencyTypes = value; }
        }
        public decimal UserMoneyAmount
        {
            get { return _userMoneyAmount; }
            set
            {
                _userMoneyAmount = value;
                NotifyOfPropertyChange(() => UserMoneyAmount);
                NotifyOfPropertyChange(() => CanCreateOfferButton);

            }
        }

        public async void AddMoneyButton()
        {
            try
            {
                await _moneysEndPoint.AddPendingMoney(new AddPendingMoneyModel{ Amount = Amount, CurrencyCode = SelectedCurrencyType.CurrencyCode});
                await _soloDB.SetAndShow("Success!", "Your money has been added to the pending list. It will be added to your account when authorized by an admin.", "Ok");
            }
            catch (Exception ex)
            {
                await _soloDB.SetAndShow("Error!", "Something has gone wrong\n" + ex.Message, "Ok");
            }
        }
        public async void BackButton()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new PreviousButtonClickedEvent());
        }
        public async void CreateOfferButton()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new CreateBuyOfferClickedEvent());
        }
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
            UserMoneyAmount = await _moneysEndPoint.GetUserMoneyByID();

            CurrencyTypes.Clear();
            CurrencyTypes.AddRange(_currencyTypesList.Currencies);
        }
        public async void RefreshBUtton()
        {
            UserMoneyAmount = await _moneysEndPoint.GetUserMoneyByID();
        }

    }
}
