using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment01.Model.Exception
{
    class TimeException : System.Exception
    {
        public TimeException(string message) : base(message) { }
    }
}
