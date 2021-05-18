
using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Models;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows;

namespace StockExchangeUserInterface.ViewModels
{
    public class CreateOfferDialogueViewModel :Screen
    {
        private readonly IWindowManager _wm;

        public CreateOfferDialogueViewModel(IWindowManager wm)
        {
            _wm = wm;
        }

        private string _itemName;

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
        private double _unitPrice;

        public double UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                NotifyOfPropertyChange(() => UnitPrice);
                NotifyOfPropertyChange(() => TotalPrice);
                NotifyOfPropertyChange(() => CanCreateOfferButton);
            }
        }

        public double TotalPrice => SellingAmount * UnitPrice;


        
        private int _sellingAmount;

        public int SellingAmount
        {
            get { return _sellingAmount; }
            set
            {
                _sellingAmount = value; 
                NotifyOfPropertyChange(() => SellingAmount);
                NotifyOfPropertyChange(() => TotalPrice);
                NotifyOfPropertyChange(() => CanCreateOfferButton);
            }
        }
        public bool CanCreateOfferButton
        {
            get
            {
                return SellingAmount <= ItemAmount && SellingAmount > 0 && UnitPrice > 0;
            }
        }

        public async void CreateOfferButton ()
        {
            await Task.CompletedTask; 
        }
        public async void BackButton()
        {
            await Task.CompletedTask;
        }

        internal Task SetAndShow(UserItemModel uim)
        {
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
        public async void Button()
        {
            await TryCloseAsync();
        }
    }
}
