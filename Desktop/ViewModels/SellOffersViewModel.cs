using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class SellOffersViewModel : Screen
    {
        private readonly IAuthorizedApiHelper _aPIHelper;
        private readonly IItemsEndPoint _itemsEnd;
        private readonly IMoneysEndPoint _moneysEndPoint;
        private readonly SoloButtonDialogBoxViewModel _sbdbvm;
        private int _amountToBuy;
        private BindableCollection<ItemTypeModel> _itemTypeBindableList = new BindableCollection<ItemTypeModel>();
        private IItemTypeListModel _itemTypeList;
        private List<GetSellOffersModel> _offersList;
        private bool _selectedItemMatchesOffers = false;
        private ItemTypeModel _selectedItemType;

        private BindableCollection<GetSellOffersModel> _sellOffersById = new BindableCollection<GetSellOffersModel>();
        private int _totalAmountInOffers;
        private decimal _userMoneyAmount;

        public SellOffersViewModel(IAuthorizedApiHelper aPIHelper, IItemTypeListModel itemTypeList, IItemsEndPoint itemsEnd, IMoneysEndPoint moneysEndPoint, SoloButtonDialogBoxViewModel sbdbvm)
        {
            _aPIHelper = aPIHelper;
            _itemTypeList = itemTypeList;
            _itemsEnd = itemsEnd;
            _moneysEndPoint = moneysEndPoint;
            _sbdbvm = sbdbvm;
        }
        public string AmountToBuyText
        {
            get { return _amountToBuy.ToString(); }
            set
            {
                int.TryParse(value, out _amountToBuy);
                NotifyOfPropertyChange(() => AmountToBuyText);
                NotifyOfPropertyChange(() => CanBuyButton);
                CalculatePrices();

            }
        }

        private void CalculatePrices()
        {
            if (TotalAmountInOffers < _amountToBuy) { PurchaseCost = 0; return; }
            int totalamount = 0;
            decimal totalPurchasePrice = 0;

            foreach (var offer in OffersList)
            {
                int addAmount = offer.Amount;
                if (totalamount >= _amountToBuy) break;
                if (totalamount + addAmount > _amountToBuy) addAmount = _amountToBuy - totalamount;
                totalPurchasePrice += addAmount * offer.UnitPrice;
                totalamount += addAmount;
            }
            PurchaseCost = totalPurchasePrice;
        }



        public BindableCollection<GetSellOffersModel> GridView => _sellOffersById;

        public BindableCollection<ItemTypeModel> ItemTypeBindableList => _itemTypeBindableList;
        public List<GetSellOffersModel> OffersList 
        {
            get { return _offersList; }
            set { _offersList = value; }
        }
        private decimal _purchaseCost;
        public decimal PurchaseCost
        {
            get => _purchaseCost;

            set
            {
                _purchaseCost = value;
                NotifyOfPropertyChange(() => PurchaseCost);
                NotifyOfPropertyChange(() => TotalCost);
                NotifyOfPropertyChange(() => ComissionFee);
                NotifyOfPropertyChange(() => CanBuyButton);
            }
        }


        public decimal ComissionFee => PurchaseCost / 100;
        public decimal TotalCost => ComissionFee + PurchaseCost;

        public bool CanBuyButton => AmountToBuyText!="" && _amountToBuy > 0 && _amountToBuy <= TotalAmountInOffers && UserMoneyAmount >= TotalCost && _selectedItemMatchesOffers;
        
        public async void BuyButton()
        {
            try
            {
                await _itemsEnd.IssueMarketOrder(new MarketOrderModel { TotalPrice = PurchaseCost, ItemAmount = _amountToBuy, ItemTypeID = SelectedItemType.ItemTypeID });
                await _sbdbvm.SetAndShow("Success","Transactions have been made", "Ok");
            }
            catch (Exception ex)
            {
                await _sbdbvm.SetAndShow("Error", ex.Message, "Ok");
            }
            finally
            {
                UserMoneyAmount = await _moneysEndPoint.GetUserMoneyByID();
                GetOffersButton();
            }
                

            
        }

        public ItemTypeModel SelectedItemType
        {
            get { return _selectedItemType; }
            set
            {
                _selectedItemMatchesOffers = false;
                _selectedItemType = value;
                NotifyOfPropertyChange(() => SelectedItemType);
                NotifyOfPropertyChange(() => CanGetOffersButton);
                NotifyOfPropertyChange(() => PurchaseCost);
                NotifyOfPropertyChange(() => ComissionFee);
                NotifyOfPropertyChange(() => TotalCost);
            }
        }

        public int TotalAmountInOffers
        {
            get { return _totalAmountInOffers; }
            set { _totalAmountInOffers = value; NotifyOfPropertyChange(() => TotalAmountInOffers); }
        }
        public decimal UserMoneyAmount
        {
            get { return _userMoneyAmount; }
            set { _userMoneyAmount = value;NotifyOfPropertyChange(() => UserMoneyAmount);}
        }
        public bool CanGetOffersButton => SelectedItemType != null;

        public async void GetOffersButton()
        {
            if (SelectedItemType == null) return;

            OffersList = await _itemsEnd.GetSellOffersByID(SelectedItemType.ItemTypeID);
            OffersList.Sort((x,y)=>x.UnitPrice.CompareTo(y.UnitPrice));
            GridView.Clear();
            GridView.AddRange(OffersList);

            _selectedItemMatchesOffers = true;

            AmountToBuyText = "";

            var temp = 0;
            OffersList.ForEach(p => temp += p.Amount);
            TotalAmountInOffers = temp;
        }
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
            _itemTypeList.ItemTypeList = await _itemsEnd.GetItemTypesInfo();
            ItemTypeBindableList.Clear();
            ItemTypeBindableList.AddRange(_itemTypeList.ItemTypeList);

            UserMoneyAmount = await _moneysEndPoint.GetUserMoneyByID();
                           
            
        }
    }
}
