using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Library.Repository
{
    public class DbResult
    {
        object _val = null;
        Status _status = Status.Ok;

        public DbResult(object value, Status status)
        {
            _val = value;
            _status = status;
        }

        public object Value 
        { 
            get { return _val; }
            private set { _val = value; }
        }

        public Status Status
        {
            get { return _status; }
            private set { _status = value; }
        }

        public T Get<T>()
        {
            if (Value is T)
                return (T)Value;

            return default(T);
        }
    }
}
