
using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using System;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace StockExchangeUserInterface.ViewModels
{
    public class CreateBuyOfferDialogueViewModel : Screen
    {
        private readonly SoloButtonDialogBoxViewModel _dialogbox;
        private readonly IItemsEndPoint _itemsEndPoint;
        private readonly IMoneysEndPoint _moneysEndPoint;
        private readonly IWindowManager _wm;
        private IEventAggregator _eventAggregator;
        private int _itemAmount;
        private string _itemName;
        private BindableCollection<ItemTypeModel> _itemTypeBindableList = new BindableCollection<ItemTypeModel>();
        private IItemTypeListModel _itemTypeList;
        private ItemTypeModel _selectedItemType;
        private int _buyingAmount;
        private UserItemModel _uim;

        private decimal _unitPrice;

        private decimal _userMoney;

        public CreateBuyOfferDialogueViewModel(IWindowManager wm, IItemsEndPoint itemsEndPoint, SoloButtonDialogBoxViewModel dialogbox, IMoneysEndPoint moneysEndPoint, IItemTypeListModel itemTypeList)
        {
            _wm = wm;
            _itemsEndPoint = itemsEndPoint;
            _dialogbox = dialogbox;
            _moneysEndPoint = moneysEndPoint;
            _itemTypeList = itemTypeList;
        }

        public int BuyingAmount
        {
            get { return _buyingAmount; }
            set
            {
                _buyingAmount = value;
                NotifyOfPropertyChange(() => BuyingAmount);
                NotifyOfPropertyChange(() => TotalPrice);
                NotifyOfPropertyChange(() => CanCreateBuyOfferButton);
            }
        }

        public bool CanCreateBuyOfferButton
        {
            get
            {
                return BuyingAmount > 0 && UnitPrice > 0 && TotalPrice <= UserMoney && SelectedItemType!=null;
            }
        }

        public int ItemAmount
        {
            get { return _itemAmount; }
            set { _itemAmount = value; NotifyOfPropertyChange(() => ItemAmount); }
        }

        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; NotifyOfPropertyChange(() => ItemName); }
        }

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
            }
        }
        public decimal TotalPrice => BuyingAmount * UnitPrice;

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                NotifyOfPropertyChange(() => UnitPrice);
                NotifyOfPropertyChange(() => TotalPrice);
                NotifyOfPropertyChange(() => CanCreateBuyOfferButton);
            }
        }
        public decimal UserMoney
        {
            get { return _userMoney; }
            set { _userMoney = value;
                NotifyOfPropertyChange(() => UserMoney);
            }
        }

        public async void BackButton()
        {
            await TryCloseAsync();
        }

        public async void CreateBuyOfferButton()
        {
            OfferModel offer = new OfferModel
            {
                Amount = BuyingAmount,
                ItemTypeID = SelectedItemType.ItemTypeID,
                UnitPrice = UnitPrice,
            };
            try
            {
                await _moneysEndPoint.CreateBuyOffer(offer);
                UserMoney = await _moneysEndPoint.GetUserMoneyByID();
                await TryCloseAsync();
                await _dialogbox.SetAndShow("Success", "Succesfully created the buy offer", "Ok");

            }
            catch
            {
                await _dialogbox.SetAndShow("Failure", "Couldn't place the buy offer", "Ok");
            }


        }

        internal Task SetAndShow(decimal userMoney)
        {
            UserMoney = userMoney;
            UnitPrice = 0;
            BuyingAmount = 0;

            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Title = "Create Buy Offer";

            return _wm.ShowDialogAsync(this, null, settings);

        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
            _itemTypeList.ItemTypeList = await _itemsEndPoint.GetItemTypesInfo();
            _itemTypeBindableList.Clear();
            _itemTypeBindableList.AddRange(_itemTypeList.ItemTypeList);

            NotifyOfPropertyChange(() => BuyingAmount);
            NotifyOfPropertyChange(() => TotalPrice);
            NotifyOfPropertyChange(() => CanCreateBuyOfferButton);
        }
    }
}