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
        // Configurable switch between db and file source for products
        RepositoryConfig _repoConf = null;

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

            // Load repository configuration
            object temp = null;

            if ((temp = ConfigurationManager.GetSection("repoConfig")) != null)
            {
                _repoConf = temp as RepositoryConfig;
            }

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
            // repo configuration does not exist
            // fallback to defauls sql server repository
            if(_repoConf == null)
            {
                builder.Register((c, p) =>
                        new SqlServerRepository(p.Named<string>("connectionId")))
                    .As<IRepository>();
            }
            else
            {
                if (_repoConf.Type == RepositoryType.SqlDb)
                {
                    SqlConfig.Initialize(_repoConf);
                    SqlConfig.UseModel<Product>(new DbProducts());
                    SqlConfig.UseModel<Category>(new DbCategories());

                    builder.Register((c, p) => new SqlServerRepository(_repoConf.Id))
                        .As<IRepository>();
                }
                else
                {
                    FileConfig.UseModel<Product>(HostingEnvironment.MapPath(Path.Combine("~/App_Data/files", "products.json")));
                    FileConfig.UseModel<Category>(HostingEnvironment.MapPath(Path.Combine("~/App_Data/files", "categories.json")));

                    builder.RegisterType<FileRepository>()
                        .As<IRepository>();
                }
            }
            // storage
            // Configurable switch between db and file source of products
            //if ((temp = ConfigurationManager.GetSection("repoConfig")) != null)
            //{
            //    conf = temp as RepositoryConfig;
            //    if (conf.Type == RepositoryType.SqlDb)
            //    {
            //        SqlConfig.Initialize(conf);
            //        SqlConfig.UseModel<Product>(new DbProducts());
            //        SqlConfig.UseModel<Category>(new DbCategories());

            //        builder.Register((c, p) => new SqlServerRepository(conf.Id))
            //            .As<IRepository>();
            //    }
            //    else
            //    {
            //        FileConfig.UseModel<Product>(HostingEnvironment.MapPath(Path.Combine("~/App_Data/files", "products.json")));
            //        FileConfig.UseModel<Category>(HostingEnvironment.MapPath(Path.Combine("~/App_Data/files", "categories.json")));

            //        builder.RegisterType<FileRepository>()
            //            .As<IRepository>();
            //    }
            //}
            //else
            //{
            //    builder.Register((c, p) =>
            //            new SqlServerRepository(p.Named<string>("connectionId")))
            //        .As<IRepository>();
            //}
        }

        public IAppServices Services
        {
            get
            {
                return _services;
            }
        }

        public RepositoryConfig RepositoryConfig
        {
            get
            {
                return _repoConf;
            }
        }
    }
}