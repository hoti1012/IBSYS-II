using Planning_Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Masterdata
{
    public class Stock : PlanningObject
    {
        /// <summary>
        /// Nummer des Lagerplatzes
        /// </summary>
        private string _stock;

        /// <summary>
        /// Anzahl im Lager
        /// </summary>
        private int _amount;

        /// <summary>
        /// Stückpreis
        /// </summary>
        private double _price;

        /// <summary>
        /// Preis gesamt
        /// </summary>
        private double _stockvalue;

        public string stock
        {
            get { return _stock; }
            set { _stock = value; }
        }

        public int amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public double price
        {
            get { return _price; }
            set { _price = value; }
        }

        public double stockvalue
        {
            get { return _stockvalue; }
            set { _stockvalue = value; }
        }

    }
}
