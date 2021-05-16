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
    public class ShellViewModel : Conductor<object> , IHandle<LogOnEvent>, IHandle<PreviousButtonClickedEvent>, IHandle<AddNewItemTypeClickedEvent>
    {
        
        IEventAggregator _events;
        SimpleContainer _container;
        AddItemViewModel _addItemVM;
        AddNewItemTypeViewModel _addNIVM;
        Stack<Screen> _visitedScreens = new Stack<Screen>();

        public ShellViewModel(IEventAggregator eventAggregator,SimpleContainer container,
            AddItemViewModel addItemVM, AddNewItemTypeViewModel addNIVM)
        {
            _container = container;
            _events = eventAggregator;
            _addItemVM = addItemVM;
            _addNIVM = addNIVM;
            
            _events.SubscribeOnBackgroundThread(this);


            ActivateItemAsync(_container.GetInstance<LoginViewModel>());
            
        }
        
        public Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            return CheckAddToScreensAndLoad(_addItemVM);
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
            if(! (s is IHasSensitiveInfo) )
                _visitedScreens.Push(s);
            return ActivateItemAsync(s);
        }

        public Task HandleAsync(AddNewItemTypeClickedEvent message, CancellationToken cancellationToken)
        {
            return CheckAddToScreensAndLoad(_addNIVM);
        }


    }
}
