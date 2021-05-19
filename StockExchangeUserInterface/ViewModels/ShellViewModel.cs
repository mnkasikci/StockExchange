using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Models;
using StockExchangeUserInterface.Models;
using StockExchangeUserInterface.ViewModelInterfaces;
using StockExchangeUserInterface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, 
        IHandle<LogOnEvent>, 
        IHandle<PreviousButtonClickedEvent>, 
        IHandle<UserWantsToRegisterEvent>, 
        IHandle<AddNewItemTypeClickedEvent>, 
        IHandle<CreateBuyOfferClickedEvent>, 
        IHandle<LoadLoginScreenEvent>
    {
        
        IEventAggregator _events;
        SimpleContainer _container;
        AddItemViewModel _addItemVM;
        AddNewItemTypeViewModel _addNIVM;
        AuthorizePendingItemViewModel _authorizePendingItemViewModel;
        private readonly UserItemsViewModel _uivm;
        private readonly AddMoneyViewModel _amvm;
        private readonly AuthorizePendingMoneyViewModel _authorizePendingMoneyViewModel;
        private readonly CreateBuyOfferDialogueViewModel _createBuyOffer;
        private readonly ShowAllTransactionsViewModel _showAllTransactionsViewModel;
        private readonly ShowUserTransactionsViewModel _showUserTransactionsViewModel;
        private readonly MyAccountViewModel _myAccountViewModel;
        private readonly ILoggedInUserModel _loggedInUserModel;
        Stack<Screen> _visitedScreens = new Stack<Screen>();

        public ShellViewModel(
            ILoggedInUserModel loggedInUserModel,
            IEventAggregator eventAggregator,
            SimpleContainer container,
            AddItemViewModel addItemVM,
            AddNewItemTypeViewModel addNIVM,
            AuthorizePendingItemViewModel authorizePendingItemViewModel,
            UserItemsViewModel uivm,
            AddMoneyViewModel amvm,
            AuthorizePendingMoneyViewModel authorizePendingMoneyViewModel,
            CreateBuyOfferDialogueViewModel createBuyOffer,

            ShowAllTransactionsViewModel showAllTransactionsViewModel,
            ShowUserTransactionsViewModel showUserTransactionsViewModel,
            MyAccountViewModel myAccountViewModel
            )
        {
            _container = container;
            _events = eventAggregator;
            _addItemVM = addItemVM;
            _addNIVM = addNIVM;
            _authorizePendingItemViewModel = authorizePendingItemViewModel;
            _uivm = uivm;
            _amvm = amvm;
            _authorizePendingMoneyViewModel = authorizePendingMoneyViewModel;
            _createBuyOffer = createBuyOffer;
            
            _showAllTransactionsViewModel = showAllTransactionsViewModel;
            _showUserTransactionsViewModel = showUserTransactionsViewModel;
            _myAccountViewModel = myAccountViewModel;
            _loggedInUserModel = loggedInUserModel;
            _events.SubscribeOnBackgroundThread(this);


            CheckAddToScreensAndLoad(_container.GetInstance<LoginViewModel>());

        }

        public Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            NotifyOfPropertyChange(() => IsAdminMenuVisible);
            NotifyOfPropertyChange(() => IsMenuBarVisible);
            return CheckAddToScreensAndLoad(_myAccountViewModel);
        }
        public async void MoneyMenu() => await CheckAddToScreensAndLoad(_amvm);
        public async void AddItemsMenu() => await CheckAddToScreensAndLoad(_addItemVM);
        public async void CreateBuyOfferMenu() => await CheckAddToScreensAndLoad(_createBuyOffer);

        public async void TransactionsMenu() => await CheckAddToScreensAndLoad(_showUserTransactionsViewModel);
        public async void PendingItemsMenu() => await CheckAddToScreensAndLoad(_authorizePendingItemViewModel);
        public async void PendingMoneysMenu() => await CheckAddToScreensAndLoad(_authorizePendingMoneyViewModel);
        public async void AllTransactionsMenu() => await CheckAddToScreensAndLoad(_showAllTransactionsViewModel);
        public async void MyAccountMenu() => await CheckAddToScreensAndLoad(_myAccountViewModel);
        public async void MyItemsMenu() => await CheckAddToScreensAndLoad(_uivm);

        public bool IsAdminMenuVisible => _loggedInUserModel.IsAdmin;
        public bool IsMenuBarVisible => _loggedInUserModel.ID != null;

        

        public Task HandleAsync(PreviousButtonClickedEvent message, CancellationToken cancellationToken)
        {
            try
            {
                _visitedScreens.Pop();
                var screenToLoad = _visitedScreens.Peek();
                return ActivateItemAsync(screenToLoad);
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }

        public Task CheckAddToScreensAndLoad(Screen s)
        {
            if (!(s is IHasSensitiveInfo))
                _visitedScreens.Push(s);
          
            return ActivateItemAsync(s);
        }

        public Task HandleAsync(UserWantsToRegisterEvent message, CancellationToken cancellationToken)
        {
            return ActivateItemAsync(_container.GetInstance<RegisterUserViewModel>());
        }

        public async Task HandleAsync(AddNewItemTypeClickedEvent message, CancellationToken cancellationToken)
        {
            await CheckAddToScreensAndLoad(_addNIVM);
        }

        public async Task HandleAsync(CreateBuyOfferClickedEvent message, CancellationToken cancellationToken)
        {
            await CheckAddToScreensAndLoad(_createBuyOffer); 

        }

        public async Task HandleAsync(LoadLoginScreenEvent message, CancellationToken cancellationToken)
        {
            await CheckAddToScreensAndLoad(_container.GetInstance<LoginViewModel>());
        }
    }
}
