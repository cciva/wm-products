using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Library
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
