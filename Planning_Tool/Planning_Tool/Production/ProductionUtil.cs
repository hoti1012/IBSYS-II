using Planning_Tool.Forecasts;
using Planning_Tool.Masterdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Core;
using Planning_Tool.Purchase;
using Planning_Tool.Time;

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
            List<Article> artList = ArticleFactory.getAllArticle();
            List<Workplace> wps = WorkplaceFactory.getAll();
            int periode = Period.getCurrentPeriod();

            //Auftragsstücklisten anlegen
            foreach (Forecast f in forecasts)
            {
                OrderBOM bom = OrderBOMFactory.create(typeof(OrderBOM), f.forecast) as OrderBOM;
                Stock stock = StockFactory.search(typeof(Stock), f.forecast) as Stock;
                Article art = ArticleFactory.search(typeof(Article),f.forecast) as Article;
                DirektSale ds = DirektSaleFactory.search(typeof(DirektSale), f.forecast) as DirektSale;

                int use = art.getUse();
                if (use == 0)
                    use = 1;
                int amount = f.currentAmount + (stock.safetyStock/use) - (art.getWaitingList()/use) - (art.getInWork()/use) - (stock.amount/use);
                if (ds != null)
                {
                    amount += ds.amount;
                }
                if (amount < 0)
                {
                    amount = 0;
                }

                OrderBOMpos oBompos = OrderBOMposFactory.create(typeof(OrderBOMpos), bom.orderBOM, art.article, "0") as OrderBOMpos;
                oBompos.amount = amount;
                oBompos.amountN1 = f.amountOne;
                oBompos.amountN2 = f.amountTwo;
                oBompos.amountN3 = f.amountThree;
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

            //Produktionspläne und Einkaufspläne erstellen
            foreach(Article a in artList)
            {
                if (a.IsProduction)
                {
                    ProductionPlan pp = ProductionPlanFactory.create(typeof(ProductionPlan), a.article) as ProductionPlan;
                    if (pp == null)
                    {
                        pp = ProductionPlanFactory.search(typeof(ProductionPlan), a.article) as ProductionPlan;
                    }
                    foreach(OrderBOMpos oBOMpos in OrderBOMposFactory.searchAllWithPos(typeof(OrderBOMpos), a.article))
                    {
                        pp.amount += oBOMpos.amount;
                    }
                    //keine Minusproduktion möglich
                    if (pp.amount < 0)
                    {
                        pp.amount = 0;
                    }
                    pp.update();
                }
                else
                {
                    PurchasePlan pp = PurchasePlanFactory.create(typeof(PurchasePlan), a.article) as PurchasePlan;
                    if (pp == null)
                    {
                        pp = PurchasePlanFactory.search(typeof(PurchasePlan), a.article) as PurchasePlan;
                    }
                    foreach (OrderBOMpos oBOMpos in OrderBOMposFactory.searchAllWithPos(typeof(OrderBOMpos), a.article))
                    {
                        pp.amountN += oBOMpos.amount;
                        pp.amountN1 += oBOMpos.amountN1;
                        pp.amountN2 += oBOMpos.amountN2;
                        pp.amountN3 += oBOMpos.amountN3;
                    }
                    if (pp.amountN < 0)
                    {
                        pp.amountN = 0;
                    }
                    Stock s = StockFactory.search(typeof(Stock),a.article) as Stock;
                    pp.stockN1 = s.amount - pp.amountN;
                    pp.stockN2 = pp.stockN1 - pp.amountN1;
                    pp.stockN3 = pp.stockN2 - pp.amountN2;
                    pp.stockN4 = pp.stockN3 - pp.amountN3;

                    //Noch offene Bestellungen beachten
                    pp.setStockWithIncommingOrdering(periode);
                    pp.update();
                }
            }

            //Bestellungen anlegen
            Ordering order = OrderingFactory.create(typeof(Ordering), periode.ToString()) as Ordering;
            order.createNeededOrderPos();
            order.calcPrice();
            order.update();

            //Kapazitätsplanung
            WaitingListPlan.createAllWaitinglistPlan();
            foreach (Workplace wp in wps)
            {
                WaitingListPlan waiting = WaitingListPlanFactory.search(typeof(WaitingListPlan),wp.workplace) as WaitingListPlan;
                CapacityPlan capaPlan = CapacityPlanFactory.create(typeof(CapacityPlan),wp.workplace) as CapacityPlan;
                foreach(WorkSchedulePos wsPos in WorkSchedulePosFactory.searchAllWithPos(typeof(WorkSchedulePos),capaPlan.capacityPlan))
                {
                    ProductionPlan proPlan = ProductionPlanFactory.search(typeof(ProductionPlan),wsPos.workSchedule) as ProductionPlan;
                    int neededTime = 0;
                    int makeReady = 0;
                    if (proPlan != null)
                    {
                        neededTime = proPlan.amount * wsPos.workTime;
                        makeReady = wsPos.makeReady;
                    }
                    capaPlan.addPos(proPlan.productionPlan, neededTime,makeReady);
                }
                if (waiting != null)
                {
                    capaPlan.neededOldTime = waiting.neededTime;
                }
                capaPlan.finalizeCapaPlan();
                capaPlan.update();
            }
        }

    }
}
