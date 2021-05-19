using Caliburn.Micro;
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
    public class ShellViewModel : Conductor<object> , IHandle<LogOnEvent>, IHandle<PreviousButtonClickedEvent>, IHandle<AddNewItemTypeClickedEvent>, IHandle<UserWantsToRegisterEvent>
    {
        
        IEventAggregator _events;
        SimpleContainer _container;
        AddItemViewModel _addItemVM;
        AddNewItemTypeViewModel _addNIVM;
        AuthorizePendingItemViewModel _authorizePendingItemViewModel;
        private readonly UserItemsViewModel _uivm;
        private readonly AddMoneyViewModel _amvm;
        private readonly AuthorizePendingMoneyViewModel _authorizePendingMoneyViewModel;
        Stack<Screen> _visitedScreens = new Stack<Screen>();

        public ShellViewModel(
            IEventAggregator eventAggregator,
            SimpleContainer container,
            AddItemViewModel addItemVM, 
            AddNewItemTypeViewModel addNIVM, 
            AuthorizePendingItemViewModel authorizePendingItemViewModel, 
            UserItemsViewModel uivm, 
            AddMoneyViewModel amvm, 
            AuthorizePendingMoneyViewModel authorizePendingMoneyViewModel)
        {
            _container = container;
            _events = eventAggregator;
            _addItemVM = addItemVM;
            _addNIVM = addNIVM;
            _authorizePendingItemViewModel = authorizePendingItemViewModel;
            _uivm = uivm;
            _amvm = amvm;
            _authorizePendingMoneyViewModel = authorizePendingMoneyViewModel;
            _events.SubscribeOnBackgroundThread(this);


            CheckAddToScreensAndLoad(_container.GetInstance<LoginViewModel>());
            
        }
        
        public Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            return CheckAddToScreensAndLoad(_authorizePendingMoneyViewModel);
            //return CheckAddToScreensAndLoad(_uivm);
            //return CheckAddToScreensAndLoad(_amvm);
            // return CheckAddToScreensAndLoad(_addItemVM);
            //return CheckAddToScreensAndLoad(_authorizePendingItemViewModel);
        }

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

            //add if statement, push an empty version if s implements IHasSensitiveInfo 
            _visitedScreens.Push(s);
            
            return ActivateItemAsync(s);
        }

        public Task HandleAsync(AddNewItemTypeClickedEvent message, CancellationToken cancellationToken)
        {
            return CheckAddToScreensAndLoad(_addNIVM);
        }

        public Task HandleAsync(UserWantsToRegisterEvent message, CancellationToken cancellationToken)
        {
            return ActivateItemAsync(_container.GetInstance<RegisterUserViewModel>());
        }
    }
}
