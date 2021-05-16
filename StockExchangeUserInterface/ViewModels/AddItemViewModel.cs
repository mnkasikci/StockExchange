using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.Models;
using StockExchangeUserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockExchangeUserInterface.ViewModels
{
    public class AddItemViewModel : Screen
    {
        
        private BindableCollection<ItemTypeModel> _itemTypeList = new BindableCollection<ItemTypeModel>();
        private IAPIHelper _aPIHelper;
        private ILoggedInUserModel _loggedInUserModel;
        private IEventAggregator _eventAggregator;

        public BindableCollection<ItemTypeModel> ItemTypeList
        {
            get { return _itemTypeList; }
            set 
            {
                _itemTypeList = value;
            }
        }

        private ItemTypeModel _selectedItemType;

        public ItemTypeModel SelectedItemType
        {
            get { return _selectedItemType; }
            set 
            { 
                _selectedItemType = value;
                if (_selectedItemType?.ID == -1)
                {
                    _selectedItemType = null;
                    _eventAggregator.PublishOnUIThreadAsync(new AddNewItemChosenEvent());
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
            await Task.Run(() => { });
        }


        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _itemTypeList.AddRange(await _aPIHelper.GetItemTypesInfo(_loggedInUserModel.Token));
            _itemTypeList.Add(new ItemTypeModel() { ID = -1, ItemTypeName = "Add New Item" });
        }
        public AddItemViewModel(IAPIHelper aPIHelper,ILoggedInUserModel loggedInUserModel,IEventAggregator eventAggregator)
        {
            _aPIHelper = aPIHelper;
            _loggedInUserModel = loggedInUserModel;
            _eventAggregator = eventAggregator;
        }
    }
}
