using Caliburn.Micro;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockExchangeUserInterface.ViewModels
{
    public class AuthorizePendingMoneyViewModel : Screen
    {
        private readonly IMoneysEndPoint _moneysEndPoint;
        private readonly SoloButtonDialogBoxViewModel _soloDB;
        private BindableCollection<PendingMoneyModel> _gridView = new BindableCollection<PendingMoneyModel>();
        private List<PendingMoneyModel> _pendingMoneysList;
        private PendingMoneyModel _selectedPendingMoney;

        public AuthorizePendingMoneyViewModel(IMoneysEndPoint moneysEndPoint, SoloButtonDialogBoxViewModel soloDB)
        {
            _moneysEndPoint = moneysEndPoint;
            _soloDB = soloDB;
        }

        public BindableCollection<PendingMoneyModel> GridView
        {
            get => _gridView;
            set
            {
                _gridView = value;
                NotifyOfPropertyChange(() => GridView);
            }
        }

        public PendingMoneyModel SelectedPendingMoney
        {
            get => _selectedPendingMoney;
            set
            {
                _selectedPendingMoney = value; 
                NotifyOfPropertyChange(() => SelectedPendingMoney);
                NotifyOfPropertyChange(() => CanRefuseMoneyButton);
                NotifyOfPropertyChange(() => CanAuthorizeMoneyutton);
            }
        }
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
            _pendingMoneysList = await _moneysEndPoint.GetAllPendingMoneys();
            _gridView.AddRange(_pendingMoneysList);
            
        }
        public bool CanAuthorizeMoneyutton => SelectedPendingMoney != null;
        public async void AuthorizeMoneyButton()
        {
            try
            {
                await _moneysEndPoint.AuthorizePendingMoney(SelectedPendingMoney);
                _pendingMoneysList = await _moneysEndPoint.GetAllPendingMoneys();
                _gridView.Clear();
                _gridView.AddRange(_pendingMoneysList);

            }
            catch
            {
                await _soloDB.SetAndShow("Error!", "Something went wrong. Can't authorize money.", "Ok");
            }
        }
        public bool CanRefuseMoneyButton => SelectedPendingMoney != null;
        public async void RefuseMoneyButton()
        {
            try
            {
                await _moneysEndPoint.RefusePendingMoney(SelectedPendingMoney);
                _pendingMoneysList = await _moneysEndPoint.GetAllPendingMoneys();
                _gridView.Clear();
                _gridView.AddRange(_pendingMoneysList);
            }
            catch
            {
                await _soloDB.SetAndShow("Error!", "Something went wrong. Can't refuse money.", "Ok");
            }
        }

    }
}
