using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Masterdata
{
    class CountObject : IComparable
    {
        public string number;

        public int count;

        public CountObject(string number, int count)
        {
            this.number = number;
            this.count = count;
        }

        public int CompareTo(CountObject obj)
        {
            if (obj.count > this.count)
            {
                return 1;
            }
            if (obj.count < this.count)
            {
                return -1;
            }
            return 0;
        }

        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj as CountObject);
        }
    }
}
