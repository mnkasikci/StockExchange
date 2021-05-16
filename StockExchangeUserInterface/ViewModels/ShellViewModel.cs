using Caliburn.Micro;
using StockExchangeUserInterface.Models;
using StockExchangeUserInterface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object> , IHandle<LogOnEvent>, IHandle<AddNewItemChosenEvent>
    {
        
        IEventAggregator _events;
        SimpleContainer _container;
        AddItemViewModel _addItemVM;
        AddNewItemTypeViewModel _addNIVM;

        public ShellViewModel(LoginViewModel loginVM, IEventAggregator eventAggregator,SimpleContainer container,
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
            return ActivateItemAsync(_addItemVM);
        }

        public Task HandleAsync(AddNewItemChosenEvent message, CancellationToken cancellationToken)
        {
            return ActivateItemAsync(_addNIVM);
        }
    }
}
