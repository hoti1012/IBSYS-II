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
        /// Löst die Auftragsstückliste vollständig auf auf
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
                    DirektSale ds = DirektSaleFactory.search(typeof(DirektSale), bomPos.bompos) as DirektSale;
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
                        amount  = ((orderBomPos.amount * bomPos.amount) + (stock.safetyStock / use)) - (art.getInWork() / use) - (art.getWaitingList() / use) - (stock.amount / use); 
                    }
                    else
                    {
                        amount = orderBomPos.amount * bomPos.amount;
                    }

                    if (ds != null)
                    {
                        amount += ds.amount;
                    }

                    OrderBOMpos newOrderBomPos = OrderBOMposFactory.create(typeof(OrderBOMpos),this._orderBOM,bomPos.bompos,orderBomPos.orderBOMpos) as OrderBOMpos;
                    newOrderBomPos.amount = amount;
                    newOrderBomPos.amountN1 = orderBomPos.amountN1 * bomPos.amount;
                    newOrderBomPos.amountN2 = orderBomPos.amountN2 * bomPos.amount;
                    newOrderBomPos.amountN3 = orderBomPos.amountN3 * bomPos.amount;
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
    }
}
