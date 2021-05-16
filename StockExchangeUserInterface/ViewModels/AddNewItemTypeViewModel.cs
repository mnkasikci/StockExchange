using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.Models;
using StockExchangeUserInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeUserInterface.ViewModels
{
    public class AddNewItemTypeViewModel : Screen
    {
        IAPIHelper _aPIHelper;
        private IItemTypeListModel _itemTypeList;
        private readonly IEventAggregator _eventAggregator;
        
        public AddNewItemTypeViewModel(IAPIHelper aPIHelper, IItemTypeListModel itemTypeList, IEventAggregator eventAggregator)
        {
            _aPIHelper = aPIHelper;
            _itemTypeList = itemTypeList;
            _eventAggregator = eventAggregator;
        }
        private string _newItemTypeName;

        public string NewTypeName
        {
            get { return _newItemTypeName; }
            set
            {
                _newItemTypeName = value;
                NotifyOfPropertyChange(() => CanAddNewItemTypeButton);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }
        private bool _itemAlreadyExists=false;
        public bool CanAddNewItemTypeButton
        { 
            get
            {
                if (NewTypeName == null || NewTypeName.Length == 0)
                    return false;
                if (_itemTypeList != null && _itemTypeList.ItemTypeList != null)
                {
                    _itemAlreadyExists = _itemTypeList.ItemTypeList.Any(p => p.ItemTypeName.Equals(NewTypeName, StringComparison.CurrentCultureIgnoreCase));
                    return !_itemAlreadyExists;
                }
                return false;
            }
        }

        public bool IsErrorVisible => _itemAlreadyExists;
        

        public async void BackButton()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new PreviousButtonClickedEvent());
        }
        public async void AddNewItemTypeButton()
        {
            await Task.Run(() => { });
        }
            
    }
}
