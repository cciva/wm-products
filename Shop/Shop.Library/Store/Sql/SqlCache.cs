﻿using Shop.Library.Store;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Shop.Library.Store
{
    internal static class SqlCache
    {
        static IDictionary<Type, ISqlStoreAdapter> _adapters = new Dictionary<Type, ISqlStoreAdapter>();
        static IDictionary<string, string> _connParams = new Dictionary<string, string>();

        internal static void RegisterAdapter<T>(ISqlStoreAdapter adapter)
        {
            if (_adapters.ContainsKey(typeof(T)))
                throw new InvalidOperationException(
                    string.Format("adapter for '{0}' is already registered", typeof(T)));

            _adapters.Add(typeof(T), adapter);
        }

        internal static ISqlStoreAdapter AdapterFor<T>()
            where T : class, new()
        {
            if (!_adapters.ContainsKey(typeof(T)))
                return null;

            return _adapters[typeof(T)];
        }

        internal static void AutoReadConnParams()
        {
            string k, v;

            for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                k = ConfigurationManager.ConnectionStrings[i].Name;
                v = ConfigurationManager.ConnectionStrings[i].ConnectionString;

                if (_connParams.ContainsKey(k))
                    _connParams[k] = v;
                else
                    _connParams.Add(k, v);
            }
        }

        internal static IDictionary<string, string> ConnParams
        {
            get { return _connParams; }
        }
    }
}
