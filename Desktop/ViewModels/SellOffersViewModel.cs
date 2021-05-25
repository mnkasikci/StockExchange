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
                NotifyOfPropertyChange(() => PurchaseCostStatus);
                NotifyOfPropertyChange(() => CanBuyButton);

            }
        }
        public int AmountToBuy
        {
            get { return _amountToBuy; }
            set{_amountToBuy = value;}
        }

        public BindableCollection<GetSellOffersModel> GridView => _sellOffersById;

        public BindableCollection<ItemTypeModel> ItemTypeBindableList => _itemTypeBindableList;
        public List<GetSellOffersModel> OffersList 
        {
            get { return _offersList; }
            set { _offersList = value; }
        }
        public decimal PurchaseCost
        {
            get
            {
                int totalamount = 0;
                decimal totalprice = 0;
                
                foreach (var offer in OffersList)
                {
                    int addAmount = offer.Amount;
                    if (totalamount >= AmountToBuy) break;
                    if (totalamount + addAmount > AmountToBuy) addAmount = AmountToBuy - totalamount;
                    totalprice += addAmount * offer.UnitPrice;
                }
                return totalprice;
            }
        }

        public string PurchaseCostStatus
        {
            get
            {
                if (!_selectedItemMatchesOffers) return "";
                if (AmountToBuy <= 0) return "";
                return PurchaseCost.ToString();
            }
        }

        public bool CanBuyButton => AmountToBuyText!="" && AmountToBuy > 0 && AmountToBuy <= TotalAmountInOffers && UserMoneyAmount >= PurchaseCost && _selectedItemMatchesOffers;
        
        public async void BuyButton()
        {
            try
            {
                await _itemsEnd.IssueMarketOrder(new MarketOrderModel { TotalPrice = PurchaseCost, ItemAmount = AmountToBuy, ItemTypeID = SelectedItemType.ItemTypeID });
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
                NotifyOfPropertyChange(() => PurchaseCost);
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
        public async void GetOffersButton()
        {
            OffersList = await _itemsEnd.GetSellOffersByID(SelectedItemType.ItemTypeID);
            _selectedItemMatchesOffers = true;

            GridView.Clear();
            GridView.AddRange(OffersList);

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
