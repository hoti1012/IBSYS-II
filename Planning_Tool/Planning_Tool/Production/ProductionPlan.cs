using Planning_Tool.Core;
using Planning_Tool.Exceptions;
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
        /// Bezeichnung
        /// </summary>
        private string _designation;

        /// <summary>
        /// Verkaufswunsch
        /// </summary>
        private int _sellwish;

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
                    if (a.IsProduction)
                    {
                            
                        pp = ProductionPlanFactory.search(typeof(ProductionPlan), a.article) as ProductionPlan;
                        if (pp == null)
                        {
                            pp = ProductionPlanFactory.create(typeof(ProductionPlan), a.article) as ProductionPlan;
                        }
                        pp._stock = a.getStockAmount();
                        pp._waitList = a.getWaitingList();
                        pp._inWork = a.getInWork();
                        pp.update();
                    }
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
                        pp._sellwish = forecast.currentAmount;
                        pp.calcProduction();
                        pp.update();
                        pp.calcSellwichBom();
                    }
                }
            }
        }

        /// <summary>
        /// Berechnet die Aufträge
        /// </summary>
        private void calcSellwichBom()
        {
            List<ProductionPlan> list = ProductionPlanFactory.getProductionPlansFromBom(this.productionPlan);
            foreach (ProductionPlan pp in list)
            {
                //TODO: Artikel die von mehreren Objecten verwendet werden Teilen
                pp._sellwish = this.production + this.waitList;
                pp.calcProduction();
                pp.update();
                pp.calcSellwichBom();
            }
        }

        /// <summary>
        /// Gibt an ob unter diesem Produktionsplan noch weitere Produktionspläne gibt
        /// </summary>
        /// <returns></returns>
        public bool hasModule()
        {
            BOM bom = BOMFactory.search(typeof(BOM), this._productionPlan) as BOM;
            if (bom != null)
                return bom.hasModule();
            return false;
        }

        /// <summary>
        /// Berechnet die Produktion
        /// </summary>
        public void calcProduction()
        {
            _production = _sellwish + _safetyStock - _stock - _waitList - _inWork;
        }

        public string productionPlan
        {
            get { return _productionPlan; }
            set { 
                
                _productionPlan = value;
                Article art = ArticleFactory.search(typeof(Article),value) as Article;
                if (art == null)
                {
                    throw new ArticleNotFoundException(value);
                }
                this._designation = art.Designation;
                this._safetyStock = art.safetyStock;
            }
        }

        public string designation
        {
            get { return _designation; }
            set { _designation = value; }
        }

        public int sellwish
        {
            get { return _sellwish; }
            set 
            { 
                _sellwish = value;
            }
        }

        public int safetyStock
        {
            get { return _safetyStock; }
            set { 
                _safetyStock = value;
            }
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
            set 
            { 
                _production = value;
            }
        }
    }
}
