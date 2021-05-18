using Caliburn.Micro;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockExchangeUserInterface.ViewModels
{
    public class AuthorizePendingItemViewModel : Screen
    {
        private readonly IItemsEndPoint _itemsEndPoint;
        private BindableCollection<PendingItemShowModel> _gridView = new BindableCollection<PendingItemShowModel>();
        private List<PendingItemShowModel> _pendingItemsList;
        private PendingItemShowModel _selectedPendingItem;

        public AuthorizePendingItemViewModel(IItemsEndPoint itemsEndPoint)
        {
            _itemsEndPoint = itemsEndPoint;
        }

        public BindableCollection<PendingItemShowModel> GridView
        {
            get => _gridView;
            set
            {
                _gridView = value;
                NotifyOfPropertyChange(() => GridView);
            }
        }

        public PendingItemShowModel SelectedPendingItem
        {
            get => _selectedPendingItem;
            set
            {
                _selectedPendingItem = value;
                NotifyOfPropertyChange(() => SelectedPendingItem);
            }
        }
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _pendingItemsList = await _itemsEndPoint.GetAllPendingItems();
            _gridView.AddRange(_pendingItemsList);

        }
    }
}
