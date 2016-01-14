using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Core;
using Planning_Tool.Time;
using Planning_Tool.Masterdata;

namespace Planning_Tool.Purchase
{
    class PurchasePlan : PlanningObject
    {
        private string _purchasePlan;

        public string purchasePlan
        {
            get { return _purchasePlan; }
            set { _purchasePlan = value; }
        }

        private int _amountN;

        public int amountN
        {
            get { return _amountN; }
            set { _amountN = value; }
        }

        private int _stockN1;

        public int stockN1
        {
            get { return _stockN1; }
            set { _stockN1 = value; }
        }

        private int _amountN1;

        public int amountN1
        {
            get { return _amountN1; }
            set { _amountN1 = value; }
        }

        private int _stockN2;

        public int stockN2
        {
            get { return _stockN2; }
            set { _stockN2 = value; }
        }

        private int _amountN2;

        public int amountN2
        {
            get { return _amountN2; }
            set { _amountN2 = value; }
        }

        private int _stockN3;

        public int stockN3
        {
            get { return _stockN3; }
            set { _stockN3 = value; }
        }

        private int _amountN3;

        public int amountN3
        {
            get { return _amountN3; }
            set { _amountN3 = value; }
        }

        private int _stockN4;

        public int stockN4
        {
            get { return _stockN4; }
            set { _stockN4 = value; }
        }

        /// <summary>
        /// Korriegiert die entsprechenden Lagerbestände anhand der noch offenen Bestellungen
        /// </summary>
        internal void setStockWithIncommingOrdering(int period)
        {
            Article art = ArticleFactory.search(typeof(Article),this._purchasePlan) as Article;
            foreach(OrderingPos op in OrderingPosFactory.searchAllWithPos(typeof(OrderingPos),this._purchasePlan))
            {
                double div = 0;
                if (op.IsOrdered)
                {
                    int besPeriod = Convert.ToInt32(op.ordering);
                    double deliverPeriod;
                    if (op.IsExpress)
                    {
                        deliverPeriod = art.DeliverTimeExpress + besPeriod;
                    }
                    else
                    {
                        deliverPeriod = art.DeliverTime + art.DiliverDeviation + besPeriod;
                    }

                    div = period - deliverPeriod;
                    if (div < 1)
                    {
                        this._stockN1 += op.Amount;
                    }else if(div >= 1 && div < 2)
                    {
                        this._stockN2 += op.Amount;
                    }else if (div >= 2 && div < 3)
                    {
                        this._stockN3 += op.Amount;
                    }else if (div >= 3 && div < 3)
                    {
                        this._stockN4 += op.Amount;
                    }
                }
            }
        }
    }
}
