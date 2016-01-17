using Planning_Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Production
{
    class DirektSale : PlanningObject
    {
        private string _direktSale;

        public string direktSale
        {
            get { return _direktSale; }
            set { _direktSale = value; }
        }

        private int _amount;

        public int amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private double _price;

        public double price
        {
            get { return _price; }
            set { _price = value; }
        }

        private double _penalty;

        public double penalty
        {
            get { return _penalty; }
            set { _penalty = value; }
        }
    }
}
