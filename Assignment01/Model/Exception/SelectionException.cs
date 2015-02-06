using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment01.Model.Exception
{
    class SelectionException : System.Exception
    {
        public SelectionException(string message) : base(message) { }
    }
}
