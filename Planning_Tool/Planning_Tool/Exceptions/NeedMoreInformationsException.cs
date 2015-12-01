using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Exceptions
{
    class NeedMoreInformationsException : Exception
    {
        public NeedMoreInformationsException(string message) : base(message) { }
    }
}
