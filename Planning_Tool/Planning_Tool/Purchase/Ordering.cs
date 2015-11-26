using Planning_Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Purchase
{
    class Ordering : PlanningObject
    {
        public static string TABLE = typeof(Ordering).Name;

        /// <summary>
        /// Nummer der Bestellung
        /// </summary>
        private string _ordering;

        /// <summary>
        /// Preis der Gesamten Bestellung
        /// </summary>
        private double price;

        /// <summary>
        /// Enthält den Preis der Bestellung
        /// </summary>
        private double mterialPrice;

        public double MterialPrice
        {
            get { return mterialPrice; }
            set { mterialPrice = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public string ordering
        {
            get { return _ordering; }
            set { _ordering = value; }
        }
    }
}
