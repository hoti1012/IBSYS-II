using Planning_Tool.Core;
using Planning_Tool.Forecasts;
using Planning_Tool.Masterdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Production
{
    class ProductionPlan : PlanningObject
    {
        /// <summary>
        /// Enthält die Artikelnummer
        /// </summary>
        private string _productionPlan;

        /// <summary>
        /// Verkaufswunsch
        /// </summary>
        private int _sellwich;

        /// <summary>
        /// Sicherheitsbestand
        /// </summary>
        private int _safetyStock;

        /// <summary>
        /// Lagerbestand
        /// </summary>
        private int _stock;

        /// <summary>
        /// In Arbeit
        /// </summary>
        private int _inWork;

        /// <summary>
        /// Warteliste
        /// </summary>
        private int _waitList;

        /// <summary>
        /// Produktion
        /// </summary>
        private int _production;

        /// <summary>
        /// Füllt die Produktionsplanungstabelle
        /// </summary>
        public static void fillPlan()
        {
            ProductionPlan pp = null;
            List<Article> artList = new List<Article>(); 
            try
            {
                artList = ArticleFactory.getAllArticle();
                foreach (Article a in artList) { 
                    pp = ProductionPlanFactory.search(typeof(ProductionPlan),a.article) as ProductionPlan;
                    if (pp == null)
                    {
                        pp = ProductionPlanFactory.create(typeof(ProductionPlan), a.article) as ProductionPlan;
                    }
                    pp.stock = a.getStockAmount();
                    pp.waitList = a.getWaitingList();
                    pp.inWork = a.getInWork();
                    pp.update();
                }
            }
            finally
            {
                //nichts tun
            }
        }

        /// <summary>
        /// Schreibt die eingetragenen Forecasts in den Produktionsplan
        /// </summary>
        public static void setForecasts()
        {
            Forecast forecast = null;
            ProductionPlan pp = null;

            for (int i = 1; i <= 3; i++)
            {
                forecast = ForecastFactory.search(typeof(Forecast), i.ToString()) as Forecast;
                if (forecast != null)
                {
                    pp = ProductionPlanFactory.search(typeof(ProductionPlan), i.ToString()) as ProductionPlan;
                    if (pp != null)
                    {
                        pp._sellwich = forecast.currentAmount;
                        pp.update();
                    }
                }
            }
        }

        public string productionPlan
        {
            get { return _productionPlan; }
            set { _productionPlan = value; }
        }

        public int sellwich
        {
            get { return _sellwich; }
            set { _sellwich = value; }
        }

        public int safetyStock
        {
            get { return _safetyStock; }
            set { _safetyStock = value; }
        }

        public int stock
        {
            get { return _stock; }
            set { _stock = value; }
        }

        public int waitList
        {
            get { return _waitList; }
            set { _waitList = value; }
        }

        public int inWork
        {
            get { return _inWork; }
            set { _inWork = value; }
        }

        public int production
        {
            get { return _production; }
            set { _production = value; }
        }
    }
}
