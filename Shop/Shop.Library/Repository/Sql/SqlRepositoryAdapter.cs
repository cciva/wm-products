using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Shop.Library.Repository
{
    public abstract class SqlRepositoryAdapter<T> : ISqlRepositoryAdapter<T>
        where T : class, new()
    {
        protected abstract IEnumerable<T> ReadData(IDbConnection connection, object filter = null);
        protected abstract Status WriteData(IDbConnection connection, T item, Operation op);

        IEnumerable<T> ISqlRepositoryAdapter<T>.Read(IDbConnection connection, object filter)
        {
            return this.ReadData(connection, filter);
        }

        Status ISqlRepositoryAdapter<T>.Write(IDbConnection connection, T item, Operation op)
        {
            return this.WriteData(connection, item, op);
        }

        IEnumerable<object> ISqlRepositoryAdapter.Read(IDbConnection connection, object filter)
        {
            return (this as ISqlRepositoryAdapter<T>).Read(connection, filter);
        }

        Status ISqlRepositoryAdapter.Write(IDbConnection connection, object item, Operation op)
        {
            return (this as ISqlRepositoryAdapter<T>).Write(connection, (T)item, op);
        }
    }
}
