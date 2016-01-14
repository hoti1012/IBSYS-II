using Planning_Tool.Core;
using Planning_Tool.Exceptions;
using Planning_Tool.Masterdata;
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

        /// <summary>
        /// Fügt eine Bestellposition zur Aktuellen Bestellung hinzu
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public OrderingPos addPos(string article)
        {
            OrderingPos posObj = null;
            Article art = null;
            
            art = ArticleFactory.search(typeof(Article),article) as Article;
            if (art == null)
            {
                throw new ArticleNotFoundException(article);
            }
            posObj = OrderingPosFactory.create(typeof(OrderingPos), this._ordering, article) as OrderingPos;

            return posObj;

        }

        /// <summary>
        /// Gibt sämtliche Bestellpositionen zu dieser Betsellung zurück
        /// </summary>
        /// <returns></returns>
        public List<OrderingPos> getPositions()
        {
            List<OrderingPos> positions = new List<OrderingPos>();
            List<PlanningPosObject> res = null;
            res = OrderingPosFactory.search(typeof(OrderingPos), this._ordering);
            foreach (PlanningPosObject p in res)
            {
                positions.Add(p as OrderingPos);
            }
            return positions;
        }

        public void calcPrice()
        {
            List<OrderingPos> positions = getPositions();
            foreach (OrderingPos p in positions)
            {
                price += p.Price;
                mterialPrice += p.mterialPrice;
            }
        }

        public string ordering
        {
            get { return _ordering; }
            set { _ordering = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public double MterialPrice
        {
            get { return mterialPrice; }
            set { mterialPrice = value; }
        }

        /// <summary>
        /// Erzeugt die Bestellpositionen anhand des PurchasePlanes
        /// </summary>
        internal void createNeededOrderPos()
        {
            foreach(PurchasePlan pp in PurchasePlanFactory.searchAll(typeof(PurchasePlan)))
            {
                Article art = ArticleFactory.search(typeof(Article), pp.purchasePlan) as Article;
                bool createOrder = false;
                bool express = false;
                int period = getNeedetPeriod(pp);
                if (period != 4)
                {
                    double deliverTime = art.DeliverTime + art.DiliverDeviation;
                    if (period > deliverTime)
                    {
                        if (period - deliverTime <= 1)
                        {
                            createOrder = true;
                        }
                    }else
                    {
                            createOrder = true;
                            express = true;
                    }
                }
                if (createOrder)
                {
                    OrderingPos op = this.addPos(art.article) as OrderingPos;
                    op.IsExpress = express;
                    op.Amount = art.Discount;
                    op.IsOrdered = false;
                    op.update();
                }
            }
        }

        private int getNeedetPeriod(PurchasePlan pp)
        {
            if (pp.stockN1 <= 0)
                return 0;
            if (pp.stockN2 <= 0)
                return 1;
            if (pp.stockN3 <= 0)
                return 2;
            if (pp.stockN4 <= 0)
                return 3;
            return 4;

        }
    }
}
