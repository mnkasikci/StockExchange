using Caliburn.Micro;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockExchangeUserInterface.ViewModels
{
    public class UserItemsViewModel : Screen
    {
        private readonly IItemsEndPoint _itemsEndPoint;
        private readonly SoloButtonDialogBoxViewModel _soloDB;
        private readonly CreateOfferDialogueViewModel _createofferVm;
        private BindableCollection<UserItemModel> _gridView = new BindableCollection<UserItemModel>();
        private List<UserItemModel> _pendingItemsList;
        private UserItemModel _selectedPendingItem;

        public UserItemsViewModel(IItemsEndPoint itemsEndPoint, SoloButtonDialogBoxViewModel soloDB, CreateOfferDialogueViewModel createofferVm)
        {
            _itemsEndPoint = itemsEndPoint;
            _soloDB = soloDB;
            _createofferVm = createofferVm;
        }

        public BindableCollection<UserItemModel> GridView
        {
            get => _gridView;
            set
            {
                _gridView = value;
                NotifyOfPropertyChange(() => GridView);
            }
        }

        public UserItemModel SelectedItem
        {
            get => _selectedPendingItem;
            set
            {
                _selectedPendingItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
                NotifyOfPropertyChange(() => CanCreateOfferButton);
            }
        }
        private async Task RefreshList()
        {
            _pendingItemsList = await _itemsEndPoint.GetUserItems();
            _gridView.Clear();
            _gridView.AddRange(_pendingItemsList);
        }
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
            await RefreshList();
        }
        public bool CanCreateOfferButton => SelectedItem != null;
        public async void CreateOfferButton()
        {
            await _createofferVm.SetAndShow(SelectedItem);
            await RefreshList();
        }

    }

}
