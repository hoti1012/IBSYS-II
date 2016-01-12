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
            //Auftragsstücklisten anlegen und Nachauflösen
            foreach (Forecast f in ForecastFactory.getAll())
            {
                OrderBOM bom = OrderBOMFactory.create(typeof(OrderBOM), f.forecast) as OrderBOM;
                Stock stock = StockFactory.search(typeof(Stock), f.forecast) as Stock;
                Article art = ArticleFactory.search(typeof(Article),f.forecast) as Article;

                if(bom == null)
                    bom = OrderBOMFactory.search(typeof(OrderBOM), f.forecast) as OrderBOM;
                int use = art.use;
                if (use == 0)
                    use = 1;
                int amount = f.currentAmount + (stock.safetyStock/use) - (art.getWaitingList()/use) - (art.getInWork()/use) - (stock.amount/use);
                if (amount < 0)
                {
                    amount = 0;
                }
                bom.amount = amount;
                bom.update();
                bom.explodeBOM();
            }
        }

    }
}
