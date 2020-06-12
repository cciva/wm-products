using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Shop.Library.Store
{
    public abstract class SqlStoreAdapter<T> : ISqlStoreAdapter<T>
        where T : class, new()
    {
        protected abstract IEnumerable<T> ReadData(IDbConnection connection, object filter = null);
        protected abstract Status WriteData(IDbConnection connection, T item, Operation op);

        IEnumerable<T> ISqlStoreAdapter<T>.Read(IDbConnection connection, object filter)
        {
            return this.ReadData(connection, filter);
        }

        Status ISqlStoreAdapter<T>.Write(IDbConnection connection, T item, Operation op)
        {
            return this.WriteData(connection, item, op);
        }

        IEnumerable<object> ISqlStoreAdapter.Read(IDbConnection connection, object filter)
        {
            return (this as ISqlStoreAdapter<T>).Read(connection, filter);
        }

        Status ISqlStoreAdapter.Write(IDbConnection connection, object item, Operation op)
        {
            return (this as ISqlStoreAdapter<T>).Write(connection, (T)item, op);
        }
    }
}
