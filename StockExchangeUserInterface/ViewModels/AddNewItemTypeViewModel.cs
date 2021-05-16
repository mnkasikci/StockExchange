using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeUserInterface.ViewModels
{
    public class AddNewItemTypeViewModel: Screen
    {
        AddItemViewModel _aivm;
        IAPIHelper _aPIHelper;

        public AddNewItemTypeViewModel(AddItemViewModel aivm,IAPIHelper aPIHelper)
        {
            _aivm = aivm;
            _aPIHelper = aPIHelper;
        }
        private string _newItemTypeName;

        public string NewTypeName
        {
            get { return _newItemTypeName; }
            set 
            {
                _newItemTypeName = value;
                NotifyOfPropertyChange(() => CanAddNewItemTypeButton);
            }
        }

        public bool CanAddNewItemTypeButton => _aivm.ItemTypeList.All(p => p.ItemTypeName != NewTypeName);

        public async void AddNewItemTypeButton()
        {
            await Task.Run(() => { });
        }
            
    }
}
