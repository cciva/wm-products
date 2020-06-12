using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Library
{
    public class Status
    {
        public Status()
        {
            Success = true;
            Error = null;
        }

        public Status(Exception error)
        {
            Success = false;
            Error = error;
        }

        public static Status Ok
        {
            get { return new Status(); }
        }

        public bool Success
        {
            get;
            private set;
        }

        public Exception Error
        {
            get;
            private set;
        }
    }
}
