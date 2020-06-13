using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Shop.Library.Repository
{
    public interface ISqlRepositoryAdapter
    {
        IEnumerable<object> Read(IDbConnection connection, object filter = null);
        Status Write(IDbConnection connection, object item, Operation op);
    }

    public interface ISqlRepositoryAdapter<T> : ISqlRepositoryAdapter
        where T : class, new()
    {
        new IEnumerable<T> Read(IDbConnection connection, object filter = null);
        Status Write(IDbConnection connection, T item, Operation op);
    }
}
