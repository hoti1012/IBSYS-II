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
        public static string TABLE = "ORDERPOS";

        /// <summary>
        /// Nummer der Bestellung
        /// </summary>
        private string ordering;

        /// <summary>
        /// artikelnummer der des zu bestellenden Artikels
        /// </summary>
        private string article;

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

        /// <summary>
        /// Berechnet den Preis der Bestellung
        /// </summary>
        public void calcPrice()
        {
            double res = 0, artPrice = 0;
            Article article;

            article = ArticleFactory.search(this.article);
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
            Price = res;
        }
            

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public bool IsExpress
        {
            get { return isExpress; }
            set { isExpress = value; }
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public string Article
        {
            get { return article; }
            set { article = value; }
        }

        public string Ordering
        {
            get { return ordering; }
            set { ordering = value; }
        }
    }
}
