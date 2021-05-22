
using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using System;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.ViewModels
{
    public class CreateSellOfferDialogueViewModel :Screen
    {
        private readonly IWindowManager _wm;
        private readonly IItemsEndPoint _itemsEndPoint;
        private readonly SoloButtonDialogBoxViewModel _dialogbox;

        public CreateSellOfferDialogueViewModel(IWindowManager wm, IItemsEndPoint itemsEndPoint, SoloButtonDialogBoxViewModel dialogbox)
        {
            _wm = wm;
            _itemsEndPoint = itemsEndPoint;
            _dialogbox = dialogbox;
        }

        private string _itemName;
        private UserItemModel _uim;

        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; NotifyOfPropertyChange(() => ItemName); }
        }
        private int _itemAmount;

        public int ItemAmount
        {
            get { return _itemAmount; }
            set { _itemAmount = value; NotifyOfPropertyChange(() => ItemAmount); }
        }
        private decimal _unitPrice;

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                NotifyOfPropertyChange(() => UnitPrice);
                NotifyOfPropertyChange(() => TotalPrice);
                NotifyOfPropertyChange(() => CanCreateSellOfferButton);
            }
        }

        public decimal TotalPrice => SellingAmount * UnitPrice;


        
        private int _sellingAmount;

        public int SellingAmount
        {
            get { return _sellingAmount; }
            set
            {
                _sellingAmount = value; 
                NotifyOfPropertyChange(() => SellingAmount);
                NotifyOfPropertyChange(() => TotalPrice);
                NotifyOfPropertyChange(() => CanCreateSellOfferButton);
            }
        }
        public bool CanCreateSellOfferButton
        {
            get
            {
                return SellingAmount <= ItemAmount && SellingAmount > 0 && UnitPrice > 0;
            }
        }
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
            NotifyOfPropertyChange(() => SellingAmount);
            NotifyOfPropertyChange(() => TotalPrice);
            NotifyOfPropertyChange(() => CanCreateSellOfferButton);
        }
        public async void CreateSellOfferButton ()
        {
            OfferModel offer = new OfferModel
            {
                Amount = SellingAmount,
                ItemTypeID = _uim.ItemTypeId,
                UnitPrice = UnitPrice,
            };
            try
            {
                await _itemsEndPoint.CreateSellOffer(offer);
                await TryCloseAsync();
                await _dialogbox.SetAndShow("Success", "Succesfully created the sell offer", "Ok");
                
            }
            catch
            {
                await _dialogbox.SetAndShow("Failure", "Couldn't place the sell offer", "Ok");
            }
            

        }
        public async void BackButton()
        {
            await TryCloseAsync();
        }

        internal Task SetAndShow(UserItemModel uim)
        {
            _uim = uim;
            ItemName = uim.ItemTypeName;
            ItemAmount = uim.Amount;
            UnitPrice = 0;
            SellingAmount = 0;

            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.Title = "Create Sell Offer";

            return _wm.ShowDialogAsync(this, null, settings);

        }
        

    }
}
