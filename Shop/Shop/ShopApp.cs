using Autofac;
using Autofac.Integration.WebApi;
using Products.Resources;
using Shop.Library;
using Shop.Library.Model;
using Shop.Library.Services;
using Shop.Library.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Products.Startup
{
    public class ShopApp
    {
        private bool _initialized = false;
        private IContainer _container;
        private IAppServices _services = null;
        static object _synclock = new object();
        static ShopApp _app = null;

        public static ShopApp Instance
        {
            get
            {
                lock (_synclock)
                {
                    if (_app == null)
                        _app = new ShopApp();
                }

                return _app;
            }
        }


        public void Initialize(HttpConfiguration config)
        {
            if (_initialized) return;

            var builder = new ContainerBuilder();

            try
            {
                builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

                RegisterAll(builder);
                _initialized = true;
            }
            catch (Exception err)
            {
                throw;
            }
            finally
            {
                if (_initialized)
                {
                    _container = builder.Build();
                    _services = new AutofacServices(_container);
                }
            }
        }

        private void RegisterAll(ContainerBuilder builder)
        {
            // repository
            SqlConfig.Initialize();
            SqlConfig.UseModel<Product>(new DbProducts());

            builder.RegisterType<SqlServerStore>()
                .As<IStore>()
                .WithParameter("connectionId", Strings.ShopDb)
                .SingleInstance();
        }

        public IAppServices Services
        {
            get
            {
                return _services;
            }
        }
    }
}