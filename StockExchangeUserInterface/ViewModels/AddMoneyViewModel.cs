﻿using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using StockExchangeUserInterface.Models;
using System;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace StockExchangeUserInterface.ViewModels
{
    public class AddMoneyViewModel : Screen
    {
        public AddMoneyViewModel (IEventAggregator eventAggregator, IMoneysEndPoint moneysEndPoint, SoloButtonDialogBoxViewModel soloButtonDialogBox, CreateBuyOfferDialogueViewModel bovm, IItemTypeListModel itemTypes)
        {
            _eventAggregator = eventAggregator;
            _moneysEndPoint = moneysEndPoint;
            _soloDB = soloButtonDialogBox;
            _createofferVm = bovm;
            _itemTypes = itemTypes;
        }


        private decimal _amount;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMoneysEndPoint _moneysEndPoint;
        private readonly SoloButtonDialogBoxViewModel _soloDB;
        private readonly CreateBuyOfferDialogueViewModel _createofferVm;
        private readonly IItemTypeListModel _itemTypes;

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value;
                NotifyOfPropertyChange(() => Amount);
                NotifyOfPropertyChange(() => CanAddMoneyButton);
            }
        }

        public bool CanAddMoneyButton => Amount>0;

        public async void AddMoneyButton()
        {
            try
            {
                await _moneysEndPoint.AddPendingMoney(new AddPendingMoneyModel{ Amount = Amount});
                await _soloDB.SetAndShow("Success!", "Your money has been added to the pending list. It will be added to your account when authorized by an admin.", "Ok");
            }
            catch (Exception ex)
            {
                await _soloDB.SetAndShow("Error!", "Something has gone wrong\n" + ex.Message, "Ok");
            }
        }
        public async void BackButton()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new PreviousButtonClickedEvent());
        }

        private decimal _userMoneyAmount;

        public decimal UserMoneyAmount
        {
            get { return _userMoneyAmount; }
            set 
            { 
                _userMoneyAmount = value;
                NotifyOfPropertyChange(() => UserMoneyAmount);
                NotifyOfPropertyChange(() => CanCreateOfferButton);
                
            }
        }

        public bool CanCreateOfferButton => UserMoneyAmount > 0;

        public async void CreateOfferButton()
        {
            await _createofferVm.SetAndShow(UserMoneyAmount);
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
            UserMoneyAmount = await _moneysEndPoint.GetUserMoneyByID();
        }


    }
}
