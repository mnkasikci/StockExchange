using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class AddNewItemTypeViewModel : Screen
    {
        
        private IItemTypeListModel _itemTypeList;
        private readonly IEventAggregator _eventAggregator;
        private readonly IItemsEndPoint _itemsEndPoint;
        private readonly SoloButtonDialogBoxViewModel _soloButton;

        public AddNewItemTypeViewModel(IAnonymousApiHelper aPIHelper, IItemTypeListModel itemTypeList, IEventAggregator eventAggregator, IItemsEndPoint itemsEndPoint, SoloButtonDialogBoxViewModel soloButton)
        {
            _itemTypeList = itemTypeList;
            _eventAggregator = eventAggregator;
            _itemsEndPoint = itemsEndPoint;
            _soloButton = soloButton;
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
            try
            {
                await _itemsEndPoint.AddNewItemType(NewTypeName);
                await _soloButton.SetAndShow("Success", "The New item type is added", "Ok");
                NewTypeName = "";
                BackButton();
            }
            catch
            {
                await _soloButton.SetAndShow("Failure", "Couldn't add new item type", "Ok");
            }
        }
            
    }
}
