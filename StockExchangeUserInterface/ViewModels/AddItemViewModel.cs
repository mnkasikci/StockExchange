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
        
        private BindableCollection<ItemTypeModel> _itemTypeBindableList = new BindableCollection<ItemTypeModel>();
        private IItemTypeListModel _itemTypeList;
        private SoloButtonDialogBoxViewModel _soloDB;
        private readonly IItemsEndPoint _itemsEnd;
        private IAuthorizedApiHelper _helper;
        private ILoggedInUserModel _loggedInUserModel;
        private IEventAggregator _eventAggregator;


        public BindableCollection<ItemTypeModel> ItemTypeList
        {
            get { return _itemTypeBindableList; }
            set 
            {
                _itemTypeBindableList = value;
            }
        }

        private ItemTypeModel _selectedItemType;

        public ItemTypeModel SelectedItemType
        {
            get { return _selectedItemType; }
            set 
            { 
                _selectedItemType = value;
                NotifyOfPropertyChange(() => SelectedItemType);
                if (_selectedItemType?.ItemTypeID == -1)
                {
                    _selectedItemType = null;
                    NotifyOfPropertyChange(() => SelectedItemType);
                    _eventAggregator.PublishOnUIThreadAsync(new AddNewItemTypeClickedEvent());
                }
            }
        }

        

        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
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


        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _itemTypeList.ItemTypeList = await _itemsEnd.GetItemTypesInfo();


            _itemTypeBindableList.Clear();
            _itemTypeBindableList.AddRange(_itemTypeList.ItemTypeList);
            _itemTypeBindableList.Add(new ItemTypeModel() { ItemTypeID = -1, ItemTypeName = "Add New Item" });
        }
        public AddItemViewModel(IAuthorizedApiHelper aPIHelper, ILoggedInUserModel loggedInUserModel
            , IEventAggregator eventAggregator, IItemTypeListModel itemTypeList,SoloButtonDialogBoxViewModel soloDB, IItemsEndPoint itemsEnd)
        {
            _itemTypeList = itemTypeList;
            _soloDB = soloDB;
            _itemsEnd = itemsEnd;
            _helper = aPIHelper;
            _loggedInUserModel = loggedInUserModel;
            _eventAggregator = eventAggregator;
        }
    }
}
