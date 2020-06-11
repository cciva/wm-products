using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Library.Storage
{
    /*
      This interface represents abstraction for 
      interacting with any kind of data storage
      using basic CRUD operations
     */
    public interface IStorage
    {
        // Load routine can have a filter to get
        // subset of data we need
        IEnumerable<object> Load(object filter = null);
        Status Insert(object obj);
        Status Update(object obj);
        Status Delete(object obj);
    }
}
