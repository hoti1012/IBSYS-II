using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Exceptions
{
    class NotFoundException : Exception
    {
        private string _identifier;

        public NotFoundException(string identifier)
        {
            _identifier = identifier;
        }

        public string identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }
    }
}
