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
            set { _orderBOM = value; }
        }


        /// <summary>
        /// Gibt alle Positionen zu dieser Stückliste zurück
        /// </summary>
        /// <returns>Eine Liste mit allen Positionen</returns>
        public List<OrderBOMpos> getAllPos()
        {
            List<OrderBOMpos> res = new List<OrderBOMpos>();
            foreach (OrderBOMpos o in PlanningObjectFactory.searchAllPos(typeof(OrderBOMpos), this._orderBOM))
            {
                res.Add(o);
            }
            return res;
        }

        public List<OrderBOMpos> getAllPosToExplode()
        {
            List<OrderBOMpos> res = new List<OrderBOMpos>();
            foreach (OrderBOMpos o in getAllPos())
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
            //Alle noch nicht Nachaufgelöste Positionen Auflösen
            List<OrderBOMpos> list = getAllPosToExplode();

            if (list == null || list.Count == 0)
                return;

            foreach(OrderBOMpos orderBomPos in list)
            {
                List<PlanningPosObject> bomPosList = BOMposFactory.search(typeof(BOMpos), orderBomPos.orderBOMpos);
                foreach (BOMpos bomPos in bomPosList)
                {
                    Article art = ArticleFactory.search(typeof(Article), bomPos.bompos) as Article;
                    Stock stock = StockFactory.search(typeof(Stock), bomPos.bompos) as Stock;
                    //Verwendung für die berechnung herrausfinden
                    int use = art.getUse();
                    if (use <= 0)
                    {
                        use = 1;
                    }

                    int amount;

                    if (art.IsProduction)
                    {
                        //Menge berechnen
                        amount = ((orderBomPos.amount * bomPos.amount) + (stock.safetyStock / use)) - (art.getInWork() / use) - (art.getWaitingList() / use) - (stock.amount / use); 
                    }
                    else
                    {
                        amount = orderBomPos.amount * bomPos.amount;
                    }

                    OrderBOMpos newOrderBomPos = OrderBOMposFactory.create(typeof(OrderBOMpos),this._orderBOM,bomPos.bompos,orderBomPos.orderBOMpos) as OrderBOMpos;
                    newOrderBomPos.amount = amount;
                    newOrderBomPos.isBom = bomPos.isModule();
                    newOrderBomPos.isExplode = false;
                    newOrderBomPos.designation = art.Designation;
                    newOrderBomPos.update();
                }
                orderBomPos.isExplode = true;
                orderBomPos.update();
            }

            explodeBOM();
        }

        /*
        /// <summary>
        /// Löst die Auftragsstückliste erstmals auf
        /// </summary>
        public void firstExplode()
        {
            List<PlanningPosObject> list = PlanningPosObjectFactory.search(typeof(BOMpos), this.orderBOM);
            foreach (BOMpos p in list)
            {
                OrderBOMpos oBom = OrderBOMposFactory.create(typeof(OrderBOMpos), this.orderBOM, p.bompos,this.orderBOM) as OrderBOMpos;
                Stock stock = StockFactory.search(typeof(Stock), p.bompos) as Stock;
                Article art = ArticleFactory.search(typeof(Article), p.bompos) as Article;
                if (art.IsProduction)
                {
                    int use = art.use;
                    if (use <= 0)
                        use = 1;
                    int amount = (this.amount * p.amount + (stock.safetyStock / use)) - (art.getWaitingList() / use) - (art.getInWork() / use) - (stock.amount / use);
                    if (amount < 0)
                        amount = 0;

                    oBom.isBom = p.isModule();
                    oBom.amount = amount;
                    oBom.isExplode = false;
                    oBom.update();
                }
                else
                {
                    int amount = this.amount * p.amount;
                    if (amount < 0)
                        amount = 0;

                    oBom.isBom = false;
                    oBom.amount = amount;
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
            //if (getAllPosToExplode().Count > 0)
            //    fullExplode();
        }
        */
    }
}
