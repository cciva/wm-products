using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Shop.Library.Store
{
    public interface ISqlStoreAdapter
    {
        IEnumerable<object> Read(IDbConnection connection, object filter = null);
        Status Write(IDbConnection connection, object item, Operation op);
    }

    public interface ISqlStoreAdapter<T> : ISqlStoreAdapter
        where T : class, new()
    {
        new IEnumerable<T> Read(IDbConnection connection, object filter = null);
        Status Write(IDbConnection connection, T item, Operation op);
    }
}
