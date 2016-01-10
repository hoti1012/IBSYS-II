using Planning_Tool.Core;
using Planning_Tool.Exceptions;
using Planning_Tool.Masterdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Production
{
    class OrderBOM : PlanningObject
    {
        private string _orderBOM;

        public string orderBOM
        {
            get { return _orderBOM; }
            set 
            {
                Article art = ArticleFactory.search(typeof(Article), value) as Article;
                if (art == null)
                {
                    throw new ArticleNotFoundException(value);
                }

                _orderBOM = value;
                _designation = art.Designation;
            }
        }

        private string _designation;

        public string designation
        {
            get { return _designation; }
            set { _designation = value; }
        }

        private int _amount;

        public int amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        /// <summary>
        /// Gibt alle Positionen zu dieser Stückliste zurück
        /// </summary>
        /// <returns>Eine Liste mit allen Positionen</returns>
        public List<OrderBOMpos> getAllPos()
        {
            List<OrderBOMpos> res = new List<OrderBOMpos>();
            foreach (PlanningPosObject p in PlanningObjectFactory.searchAllPos(typeof(OrderBOMpos), this._orderBOM))
            {
                res.Add(p as OrderBOMpos);
            }
            return res;
        }

        public List<OrderBOMpos> getAllPosToExplode()
        {
            List<OrderBOMpos> res = new List<OrderBOMpos>();
            foreach (OrderBOMpos o in this.getAllPos())
            {
                if(o.isBom && !o.isExplode)
                    res.Add(o);
            }
            return res;
        }

        /// <summary>
        /// Löst die Auftragsstückliste auf
        /// </summary>
        public void explodeBOM()
        {
            firstExplode();
            fullExplode();
        }

        /// <summary>
        /// Löst die Auftragsstückliste erstmals auf
        /// </summary>
        public void firstExplode()
        {
            foreach (PlanningPosObject p in PlanningPosObjectFactory.search(typeof(BOMpos),this.orderBOM))
            {
                BOMpos bom = p as BOMpos;
                OrderBOMpos oBom = OrderBOMposFactory.create(typeof(OrderBOMpos), this.orderBOM, bom.bompos) as OrderBOMpos;
                Stock stock = StockFactory.search(typeof(Stock), bom.bompos) as Stock;
                Article art = ArticleFactory.search(typeof(Article), bom.bompos) as Article;
                if (bom.isModule())
                {
                    int use = art.getUse().Count;
                    if (use <= 0)
                        use = 1;
                    int amount = (this.amount * bom.amount + (stock.safetyStock / use)) - (art.getWaitingList() / use) - (art.getInWork() / use) - (stock.amount / use);
                    if (amount < 0)
                        amount = 0;

                    oBom.dependence = null;
                    oBom.isBom = true;
                    oBom.amount = amount;
                    oBom.isExplode = false;
                    oBom.update();
                }
                else
                {
                    int use = art.getUse().Count;
                    if (use <= 0)
                        use = 1;
                    int amount = this.amount * bom.amount;
                    if (amount < 0)
                        amount = 0;

                    oBom.dependence = null;
                    oBom.isBom = false;
                    oBom.amount = amount;
                    oBom.isExplode = false;
                    oBom.update();
                }
            }
        }

        /// <summary>
        /// Löst die Stückliste komplett auf
        /// </summary>
        public void fullExplode()
        {
            foreach(OrderBOMpos pos in getAllPosToExplode())
            {
                pos.explode();
            }
            if (getAllPosToExplode().Count > 0)
                fullExplode();
        }
    }
}
