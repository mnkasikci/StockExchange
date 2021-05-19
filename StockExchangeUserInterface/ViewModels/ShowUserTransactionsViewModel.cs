using Caliburn.Micro;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using StockExchangeUserInterface.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockExchangeUserInterface.ViewModels
{
    public class ShowUserTransactionsViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly SoloButtonDialogBoxViewModel _soloDB;
        private readonly ITransactionsEndPoint _transactionsEndPoint;
        private BindableCollection<TransactionModel> _gridView = new BindableCollection<TransactionModel>();
        private TransactionModel _selectedTransaction;
        private List<TransactionModel> _transactionsList;
        public ShowUserTransactionsViewModel(ITransactionsEndPoint transactionsEndPoint, IEventAggregator eventAggregator)
        {
            _transactionsEndPoint = transactionsEndPoint;
            _eventAggregator = eventAggregator;
        }
        public BindableCollection<TransactionModel> GridView
        {
            get => _gridView;
            set
            {
                _gridView = value;
                NotifyOfPropertyChange(() => GridView);
            }
        }
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
            RefreshButton();
        }

        public async void RefreshButton()
        {
            _gridView.Clear();
            _transactionsList = await _transactionsEndPoint.GetTransactionsByID();
            _gridView.AddRange(_transactionsList);
        }
        public async void BackButton()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new PreviousButtonClickedEvent());
        }


    }
}
