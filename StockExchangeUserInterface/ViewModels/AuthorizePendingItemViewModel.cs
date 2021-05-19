using Caliburn.Micro;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockExchangeUserInterface.ViewModels
{
    public class AuthorizePendingItemViewModel : Screen
    {
        private readonly IItemsEndPoint _itemsEndPoint;
        private readonly SoloButtonDialogBoxViewModel _soloDB;
        private BindableCollection<PendingItemModel> _gridView = new BindableCollection<PendingItemModel>();
        private List<PendingItemModel> _pendingItemsList;
        private PendingItemModel _selectedPendingItem;

        public AuthorizePendingItemViewModel(IItemsEndPoint itemsEndPoint, SoloButtonDialogBoxViewModel soloDB)
        {
            _itemsEndPoint = itemsEndPoint;
            _soloDB = soloDB;
        }

        public BindableCollection<PendingItemModel> GridView
        {
            get => _gridView;
            set
            {
                _gridView = value;
                NotifyOfPropertyChange(() => GridView);
            }
        }

        public PendingItemModel SelectedPendingItem
        {
            get => _selectedPendingItem;
            set
            {
                _selectedPendingItem = value; 
                NotifyOfPropertyChange(() => SelectedPendingItem);
                NotifyOfPropertyChange(() => CanRefuseItemButton);
                NotifyOfPropertyChange(() => CanAuthorizeItemButton);
            }
        }
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
            RefreshButton();

        }
        public bool CanAuthorizeItemButton => SelectedPendingItem != null;
        public async void AuthorizeItemButton()
        {
            try
            {
                await _itemsEndPoint.AuthorizePendingItem(SelectedPendingItem);
                RefreshButton();

            }
            catch
            {
                await _soloDB.SetAndShow("Error!", "Something went wrong. Can't authorize item.", "Ok");
            }
        }
        public bool CanRefuseItemButton => SelectedPendingItem != null;
        public async void RefuseItemButton()
        {
            try
            {
                await _itemsEndPoint.RefusePendingItem(SelectedPendingItem);
                RefreshButton();
            }
            catch
            {
                await _soloDB.SetAndShow("Error!", "Something went wrong. Can't refuse item.", "Ok");
            }
        }
        public async void RefreshButton()
        {
            _pendingItemsList = await _itemsEndPoint.GetAllPendingItems();
            _gridView.Clear();
            _gridView.AddRange(_pendingItemsList);
        }

    }
}
