using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Library.Services
{
    public class AutofacServices : IAppServices
    {
        private IContainer _container = null;
        public AutofacServices(IContainer container)
        {
            _container = container;
        }
        public T Get<T>()
        {
            return _container.Resolve<T>();
        }

        public T Get<T>(string name)
        {
            return _container.ResolveNamed<T>(name);
        }

        public T GetByParam<T>(string key, object value)
        {
            return _container.Resolve<T>(new NamedParameter(key, value));
        }
    }
}