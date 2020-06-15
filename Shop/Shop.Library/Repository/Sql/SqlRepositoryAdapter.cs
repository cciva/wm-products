using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Shop.Library.Repository
{
    public abstract class SqlRepositoryAdapter<T> : ISqlRepositoryAdapter<T>
        where T : class, new()
    {
        protected abstract IEnumerable<T> ReadData(
            IDbConnection connection, Predicate<T> filter = null);
        protected abstract Status WriteData(
            IDbConnection connection, T item, Operation op, Predicate<T> where = null);

        IEnumerable<T> ISqlRepositoryAdapter<T>.Read(IDbConnection connection, Predicate<T> filter)
        {
            return this.ReadData(connection, filter);
        }

        Status ISqlRepositoryAdapter<T>.Write(IDbConnection connection, T item, Operation op, Predicate<T> where)
        {
            return this.WriteData(connection, item, op, where);
        }

        IEnumerable<object> ISqlRepositoryAdapter.Read(IDbConnection connection, object filter)
        {
            return (this as ISqlRepositoryAdapter<T>).Read(connection, (Predicate<T>)filter);
        }

        Status ISqlRepositoryAdapter.Write(IDbConnection connection, object item, Operation op, object where)
        {
            return (this as ISqlRepositoryAdapter<T>).Write(connection, (T)item, op, (Predicate<T>)where);
        }
    }
}
