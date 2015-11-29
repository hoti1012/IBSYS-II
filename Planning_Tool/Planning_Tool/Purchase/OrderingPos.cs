using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Masterdata;
using Planning_Tool.Core;

namespace Planning_Tool.Purchase
{
    class OrderingPos : PlanningPosObject
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
        /// Enthält den Materialpreis
        /// </summary>
        private double _mterialPrice;


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
            calcPrice();
            base.update();
            calcHead();
        }

        /// <summary>
        /// Berechnet den Preis der Bestellung
        /// </summary>
        public void calcPrice()
        {
            double res = 0, artPrice = 0;
            Article article;

            article = ArticleFactory.search(typeof(Article),this._orderingpos) as Article;
            if (article != null)
            {
                artPrice = article.Price;
                if (amount >= article.Discount)
                {
                    artPrice *= 0.9;
                }

                res = article.Price * this.Amount;
                _mterialPrice = res;
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

        //TODO: preis wird nicht berechnet
        public void calcHead()
        {
           Ordering order = this.getHead(typeof(Ordering)) as Ordering;
           order.calcPrice();
           order.update();
        }

        public Ordering getHead()
        {
            Ordering ordering = null;
            ordering = base.getHead(typeof(Ordering)) as Ordering;

            return ordering;
        }


        public string ordering
        {
            get { return _ordering; }
            set { _ordering = value; }
        }

        public string orderingpos
        {
            get { return _orderingpos; }
            set
            {
                _orderingpos = value;
            }
        }

        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
            }
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
            set { price = value; }
        }

        public double mterialPrice
        {
            get { return _mterialPrice; }
            set { _mterialPrice = value; }
        }

        public bool IsExpress
        {
            get { return isExpress; }
            set 
            { 
                isExpress = value;
            }
        }
    }
}
