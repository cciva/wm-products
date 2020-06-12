using Shop.Library.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Library.Store
{
    public static class SqlConfig
    {
        public static void Initialize()
        {
            SqlCache.AutoReadConnParams();
        }

        public static void UseModel<T>(ISqlStoreAdapter adapter)
        {
            SqlCache.RegisterAdapter<T>(adapter);
        }
    }
}
