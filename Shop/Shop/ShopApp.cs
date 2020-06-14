using Autofac;
using Autofac.Integration.WebApi;
using Shop.Library;
using Shop.Library.Model;
using Shop.Library.Services;
using Shop.Library.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Hosting;
using System.IO;

namespace Shop
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
            RepositoryConfig conf = null;
            object temp = null;

            // storage
            // Configurable switch between db and file source of products
            if ((temp = ConfigurationManager.GetSection("repoConfig")) != null)
            {
                conf = temp as RepositoryConfig;
                if (conf.Type == RepositoryType.SqlDb)
                {
                    SqlConfig.Initialize(conf);
                    SqlConfig.UseModel<Product>(new DbProducts());
                    SqlConfig.UseModel<Category>(new DbCategories());

                    builder.Register((c, p) => new SqlServerRepository(conf.Id))
                        .As<IRepository>();
                }
                else
                {
                    string filepath = HostingEnvironment.MapPath(Path.Combine("~/App_Data/files", conf.Parameters));
                    builder.Register((c, p) => new FileRepository(filepath))
                        .As<IRepository>();
                }
            }
            else
            {
                builder.Register((c, p) =>
                        new SqlServerRepository(p.Named<string>("connectionId")))
                    .As<IRepository>();
            }
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