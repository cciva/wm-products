using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Library.Repository
{
    public static class FileConfig
    {
        private static IDictionary<Type, string> _modelmap = new Dictionary<Type, string>();

        public static void UseModel<T>(string fileSource)
        {
            if (!_modelmap.ContainsKey(typeof(T)))
            {
                _modelmap.Add(typeof(T), fileSource);
            }
        }

        internal static IDictionary<Type, string> ModelMap
        {
            get { return _modelmap; }
        }
    }
}
