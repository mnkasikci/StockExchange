using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using StockExchangeUserInterface.Models;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows;

namespace StockExchangeUserInterface.ViewModels
{
    public class AddItemViewModel : Screen
    {

        private readonly IItemsEndPoint _itemsEnd;
        private int _amount;
        private IEventAggregator _eventAggregator;
        private BindableCollection<ItemTypeModel> _itemTypeBindableList = new BindableCollection<ItemTypeModel>();
        private IItemTypeListModel _itemTypeList;
        private ItemTypeModel _selectedItemType;
        private SoloButtonDialogBoxViewModel _soloDB;
        public AddItemViewModel(IAuthorizedApiHelper aPIHelper, ILoggedInUserModel loggedInUserModel
            , IEventAggregator eventAggregator, IItemTypeListModel itemTypeList, SoloButtonDialogBoxViewModel soloDB, IItemsEndPoint itemsEnd)
        {
            _itemTypeList = itemTypeList;
            _soloDB = soloDB;
            _itemsEnd = itemsEnd;
            _eventAggregator = eventAggregator;
        }

        public int Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                NotifyOfPropertyChange(() => CanAddItemButton);
                NotifyOfPropertyChange(() => Amount);
            }
        }

        public bool CanAddItemButton => Amount > 0;

        public BindableCollection<ItemTypeModel> ItemTypeList
        {
            get { return _itemTypeBindableList; }
            set 
            {
                _itemTypeBindableList = value;
            }
        }
        public ItemTypeModel SelectedItemType
        {
            get { return _selectedItemType; }
            set 
            { 
                _selectedItemType = value;
                NotifyOfPropertyChange(() => SelectedItemType);
                NotifyOfPropertyChange(() => CanAddItemButton);
                if (_selectedItemType?.ItemTypeID == -1)
                {
                    _selectedItemType = null;
                    NotifyOfPropertyChange(() => SelectedItemType);
                    _eventAggregator.PublishOnUIThreadAsync(new AddNewItemTypeClickedEvent());
                }
            }
        }
        public async void AddItemButton()
        {
            
            try
            {
                await _itemsEnd.AddPendingItem(new AddPendingItemModel { Amount = Amount, ItemTypeID = SelectedItemType.ItemTypeID });
                await _soloDB.SetAndShow("Success!", "Your item has been added to the pending list. It will be added to your inventory when authorized by an admin.", "Ok");
            }
            catch(Exception ex)
            {
                await _soloDB.SetAndShow("Error!", "Something has gone wrong\n"+ex.Message, "Ok");
            }


        }

        public async void BackButton()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new PreviousButtonClickedEvent());
        }


        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _itemTypeList.ItemTypeList = await _itemsEnd.GetItemTypesInfo();


            _itemTypeBindableList.Clear();
            _itemTypeBindableList.AddRange(_itemTypeList.ItemTypeList);
            _itemTypeBindableList.Add(new ItemTypeModel() { ItemTypeID = -1, ItemTypeName = "Add New Item" });
        }
    }
}
