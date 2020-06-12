using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Library.Services
{
    public interface IAppServices
    {
        T Get<T>();
        T Get<T>(string name);
        T GetByParam<T>(string key, object value);
    }
}