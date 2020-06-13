using Shop.Library.Model;
using Shop.Library.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Library.Repository
{
    public static class SqlConfig
    {
        public static void Initialize(RepositoryConfig conf = null)
        {
            SqlCache.AutoReadConnParams();
            if(conf != null)
            {
                SqlCache.AddConnParam(conf.Id, conf.Parameters);
            }
        }

        public static void UseModel<T>(ISqlRepositoryAdapter adapter)
        {
            SqlCache.RegisterAdapter<T>(adapter);
        }
    }
}
