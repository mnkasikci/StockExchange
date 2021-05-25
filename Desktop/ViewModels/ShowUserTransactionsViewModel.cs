using Caliburn.Micro;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using Desktop.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using StockExchangeDesktopUI.Library.Helpers;
using System.Linq;

namespace Desktop.ViewModels
{
    public class ShowUserTransactionsViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly SoloButtonDialogBoxViewModel _sbdbvm;
        private readonly ILoggedInUserModel _loggedInUserModel;
        private readonly ITransactionsEndPoint _transactionsEndPoint;
        private BindableCollection<TransactionModel> _gridView = new BindableCollection<TransactionModel>();
        private List<TransactionModel> _transactionsList;
        private bool _includeMySales=true;
        private bool _includeMyBuys=true;

        public ShowUserTransactionsViewModel(ITransactionsEndPoint transactionsEndPoint, IEventAggregator eventAggregator, SoloButtonDialogBoxViewModel sbdbvm, ILoggedInUserModel loggedInUserModel)
        {
            _transactionsEndPoint = transactionsEndPoint;
            _eventAggregator = eventAggregator;
            _sbdbvm = sbdbvm;
            _loggedInUserModel = loggedInUserModel;
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
        public bool IncludeMySales
        {
            get => _includeMySales;
            set
            {
                _includeMySales = value;
                if (!IncludeMyBuys && !IncludeMySales) IncludeMyBuys = true;
                NotifyOfPropertyChange(() => IncludeMySales);
            }
        }
        public bool IncludeMyBuys
        {
            get => _includeMyBuys;
            set
            {
                _includeMyBuys = value;
                if (!IncludeMyBuys && !IncludeMySales) IncludeMySales = true;
                NotifyOfPropertyChange(() => IncludeMyBuys);
            }
        }
        public async void SaveToFileButton()
        {
            SaveFileDialog sfd = new();
            sfd.OverwritePrompt = true;
            sfd.Filter = "Excel Spreadsheet Files (*.xlsx)|*.xlsx";
            
            List<TransactionSaveToFileModel> filteredAndCutResults = new();

            foreach (var item in _transactionsList)
                if(item.BuyerID == _loggedInUserModel.ID && IncludeMyBuys ||
                   item.SellerId==_loggedInUserModel.ID && IncludeMySales)
                    filteredAndCutResults.Add(new TransactionSaveToFileModel(item));

            if (sfd.ShowDialog() == true)
            {
                try
                {
                    SpreadSheetCreator.Create<TransactionSaveToFileModel>(sfd.FileName, filteredAndCutResults);
                    await _sbdbvm.SetAndShow("Success!", "Your transactions are saved to file: " + sfd.FileName, "Ok");
                }
                catch (Exception ex)
                {
                    await _sbdbvm.SetAndShow("Error!", ex.Message, "Ok");
                }
            }

        }


    }
}
