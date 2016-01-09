using Planning_Tool.Core;
using Planning_Tool.Exceptions;
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
        /// Beschreibung des Artikels
        /// </summary>
        private string _designation;

        /// <summary>
        /// Gibt die Verwendung an
        /// </summary>
        private int _use;

        /// <summary>
        /// Anzahl im Lager
        /// </summary>
        private int _amount;

        /// <summary>
        /// Gibt den sicherheitsbestand an
        /// </summary>
        private int _safetyStock;

        /// <summary>
        /// Stückpreis
        /// </summary>
        private double _price;

        /// <summary>
        /// Preis gesamt
        /// </summary>
        private double _stockvalue;

        public static double getCompleteStockValue()
        {
            double res = 0;
            List<Stock> stock = StockFactory.getAll();
            foreach(Stock s in stock){
                res += s._stockvalue;
            }
            return res;
        }

        public string stock
        {
            get { return _stock; }
            set 
            { 
                if (value != null)
                {
                    Article art = ArticleFactory.search(typeof(Article), value) as Article;
                    if (art == null)
                        throw new ArticleNotFoundException(value);

                    _stock = value;
                    _designation = art.Designation;
                    _use = art.getUse().Count;
                    _safetyStock = art.safetyStock;
                }
            }
        }

        public string designation
        {
            get { return _designation; }
            set { _designation = value; }
        }

        public int use
        {
            get { return _use; }
            set { _use = value; }
        }

        public int amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public int safetyStock
        {
            get { return _safetyStock; }
            set { _safetyStock = value; }
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
