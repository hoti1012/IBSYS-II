using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Masterdata;

namespace Planning_Tool.Purchase
{
    class OrderingPos
    {
        public static string TABLE = typeof(OrderingPos).Name;

        /// <summary>
        /// Nummer der Bestellung
        /// </summary>
        private string _ordering;

        /// <summary>
        /// artikelnummer des zu bestellenden Artikels
        /// </summary>
        private string _orderingpos;

        /// <summary>
        /// Beinhalet die Menge
        /// </summary>
        private int amount;

        /// <summary>
        /// Kenner ob die Bestellposition eine Express bestellung ist
        /// </summary>
        private bool isExpress;

        /// <summary>
        /// Enthält den Peis dieser Position
        /// </summary>
        private double price;

        /// <summary>
        /// Gibt an ob die Bestellung bereits getätigt wurde
        /// </summary>
        private bool isOrdered;

        /// <summary>
        /// gibt die geplante Ankunft an
        /// </summary>
        private double arrivals;

        public void update()
        {
            OrderingPosFactory.update(this);
        }

        /// <summary>
        /// Berechnet den Preis der Bestellung
        /// </summary>
        public void calcPrice()
        {
            double res = 0, artPrice = 0;
            Article article;

            article = ArticleFactory.search(this._orderingpos);
            if (article != null)
            {
                artPrice = article.Price;
                if (amount >= article.Discount)
                {
                    artPrice *= 0.9;
                }

                res = article.Price * this.Amount;
                if (isExpress)
                {
                    res += article.OrderPriceExpress;
                }
                else
                {
                    res += article.OrderPriceNormal;
                }
            }
            price = res;
        }

        public double Arrivals
        {
            get { return arrivals; }
            set { arrivals = value; }
        }

        public bool IsOrdered
        {
            get { return isOrdered; }
            set { isOrdered = value; }
        }   

        public double Price
        {
            get { return price; }

            //Der Preis wird bei änderungen am Object automatisch gesetzt
            //set { price = value; }
        }

        public bool IsExpress
        {
            get { return isExpress; }
            set 
            { 
                isExpress = value;
                calcPrice();
            }
        }

        public int Amount
        {
            get { return amount; }
            set 
            { 
                amount = value;
                calcPrice();
            }
        }

        public string orderingpos
        {
            get { return _orderingpos; }
            set { 
                _orderingpos = value;
                calcPrice();
            }
        }

        public string ordering
        {
            get { return _ordering; }
            set { _ordering = value; }
        }
    }
}
