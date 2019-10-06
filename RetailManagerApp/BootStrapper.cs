﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RetailManagerApp.ViewModels;

namespace RetailManagerApp
{
    public class BootStrapper :BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public BootStrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.Instance(_container);
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            GetType().Assembly.GetTypes()
                .Where(type=>type.IsClass)
                .Where(type=>type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(vmType=>_container.RegisterPerRequest(
                    vmType,vmType.ToString(),vmType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);  
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance); 
        }
    }
}
