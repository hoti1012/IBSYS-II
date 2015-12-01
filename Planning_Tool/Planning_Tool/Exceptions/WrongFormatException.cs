using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Exceptions
{
    public class WrongFormatException : Exception
    {
        public WrongFormatException(string message) : base(message) { }
    }
}
