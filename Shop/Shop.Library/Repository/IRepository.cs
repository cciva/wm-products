using Shop.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Library.Repository
{
    /*
      This interface represents abstraction for 
      interacting with multiple kinds of data storage
      using basic CRUD operations
     */
    public interface IRepository
    {
        IEnumerable<T> Load<T>(Predicate<T> filter = null) where T : class, new();
        Status Insert<T>(T obj) where T : class, new();
        Status Update<T>(T obj, Predicate<T> where = null) where T : class, new();
        Status Delete<T>(T obj, Predicate<T> where = null) where T : class, new();
        // For count, if exists etc.
        DbResult Execute<T>(Operation op, IDictionary<string, object> args = null) where T : class, new();

        RepositoryType Kind { get; }
    }
}
