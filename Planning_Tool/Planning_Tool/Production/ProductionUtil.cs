using Planning_Tool.Forecasts;
using Planning_Tool.Masterdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Production
{
    class ProductionUtil
    {
        /// <summary>
        /// Führt die Planung durch
        /// </summary>
        public static void startPlanning()
        {
            List<Forecast> forecasts = ForecastFactory.getAll();
            List<OrderBOM> createdBoms = new List<OrderBOM>();
            //Auftragsstücklisten anlegen
            foreach (Forecast f in forecasts)
            {
                OrderBOM bom = OrderBOMFactory.create(typeof(OrderBOM), f.forecast) as OrderBOM;
                Stock stock = StockFactory.search(typeof(Stock), f.forecast) as Stock;
                Article art = ArticleFactory.search(typeof(Article),f.forecast) as Article;

                int use = art.getUse();
                if (use == 0)
                    use = 1;
                int amount = f.currentAmount + (stock.safetyStock/use) - (art.getWaitingList()/use) - (art.getInWork()/use) - (stock.amount/use);
                if (amount < 0)
                {
                    amount = 0;
                }

                OrderBOMpos oBompos = OrderBOMposFactory.create(typeof(OrderBOMpos), bom.orderBOM, art.article, "0") as OrderBOMpos;
                oBompos.amount = amount;
                oBompos.designation = art.Designation;
                oBompos.isExplode = false;
                oBompos.isBom = true;
                oBompos.update();
                createdBoms.Add(bom);
            }

            //Auftragsstücklisten nachauflösen
            foreach (OrderBOM o in createdBoms)
            {
                o.explodeBOM();
            }

            //Produktionspläne erstellen
        }

    }
}
