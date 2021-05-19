using Caliburn.Micro;
using StockExchangeDesktopUI.Library.Api;
using StockExchangeDesktopUI.Library.EndPoints;
using StockExchangeDesktopUI.Library.Models;
using StockExchangeDesktopUI.ViewModels;
using StockExchangeUserInterface.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StockExchangeDesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
                PasswordBoxHelper.BoundPasswordProperty,
                "Password",
                "PasswordChanged");
        }

        protected override void Configure()
        {
            
            _container.Instance(_container)
                .PerRequest<IUserEndPoint,UserEndPoint>()
                .PerRequest<IItemsEndPoint,ItemsEndPoint>()
                .PerRequest<IMoneysEndPoint,MoneysEndPoint>();

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IAnonymousApiHelper, AnonymousApiHelper>()
                .Singleton<IAuthorizedApiHelper, AuthorizedApiHelper>()
                .Singleton<ILoggedInUserModel, LoggedInUserModel>()
                .Singleton<IItemTypeListModel, ItemTypeListModel>();

            


            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));

            EventManager.RegisterClassHandler(typeof(TextBox),
                TextBox.GotFocusEvent,
                new RoutedEventHandler(AutoSelectorOnFocus.TextBox_GotFocus));

            EventManager.RegisterClassHandler(typeof(PasswordBox),
                PasswordBox.GotFocusEvent,
                new RoutedEventHandler(AutoSelectorOnFocus.PasswordBox_GotFocus));

        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
        protected override object GetInstance(Type service, string key) => _container.GetInstance(service, key);
        protected override IEnumerable<object> GetAllInstances(Type service) => _container.GetAllInstances(service);

        protected override void BuildUp(object instance) => _container.BuildUp(instance);
    }
}
