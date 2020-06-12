using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Library.Store
{
    /*
      This interface represents abstraction for 
      interacting with multiple kinds of data storage
      using basic CRUD operations
     */
    public interface IStore
    {
        // Load routine can have a filter to get
        // subset of data we need
        IEnumerable<T> Load<T>(object filter = null) where T : class, new();
        Status Insert<T>(T obj) where T : class, new();
        Status Update<T>(T obj) where T : class, new();
        Status Delete<T>(T obj) where T : class, new();
    }
}
