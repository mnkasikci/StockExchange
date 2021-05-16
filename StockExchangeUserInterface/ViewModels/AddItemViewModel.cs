using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.Models;
using StockExchangeUserInterface.Models;
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
        private IAPIHelper _aPIHelper;
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
                if (_selectedItemType?.ID == -1)
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
                                   
            await _soloDB.SetAndShow("Header", "Content", "ButtonContent");
            
        }


        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _itemTypeList.ItemTypeList = await _aPIHelper.GetItemTypesInfo(_loggedInUserModel.Token);


            _itemTypeBindableList.Clear();
            _itemTypeBindableList.AddRange(_itemTypeList.ItemTypeList);
            _itemTypeBindableList.Add(new ItemTypeModel() { ID = -1, ItemTypeName = "Add New Item" });
        }
        public AddItemViewModel(IAPIHelper aPIHelper, ILoggedInUserModel loggedInUserModel
            , IEventAggregator eventAggregator, IItemTypeListModel itemTypeList,SoloButtonDialogBoxViewModel soloDB)
        {
            _itemTypeList = itemTypeList;

            _soloDB = soloDB;
            _aPIHelper = aPIHelper;
            _loggedInUserModel = loggedInUserModel;
            _eventAggregator = eventAggregator;
        }
    }
}
