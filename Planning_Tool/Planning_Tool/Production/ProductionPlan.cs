using Planning_Tool.Core;
using Planning_Tool.Exceptions;
using Planning_Tool.Forecasts;
using Planning_Tool.Masterdata;
using System;
using System.Collections;
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
        /// gibt an wie oft der Artikel Produziert wird
        /// </summary>
        private int _amount;

        /// <summary>
        /// Gibt die Reihenfolge an
        /// </summary>
        private int _dependence;

        public int dependence
        {
            get { return _dependence; }
            set { _dependence = value; }
        }

        public static int getProductionCount()
        {
            string sql = "SELECT * FROM ProductionPlan WHERE amount > 0";
            List<PlanningObject> res = ProductionPlanFactory.select(typeof(ProductionPlan), sql);
            if (res != null)
                return res.Count;
            return 0;
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
            }
        }

        public string designation
        {
            get { return _designation; }
            set { _designation = value; }
        }

        public int amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public int getWaitlist()
        {
            Article art = ArticleFactory.search(typeof(Article), _productionPlan) as Article;
            return art.getWaitingList() + art.getInWork();
        }

        
        public static void orderPlan()
        {
            List<ProductionPlan> all = ProductionPlanFactory.getAll();
            List<ProductionPlan> sortet = new List<ProductionPlan>();
            while(all.Count > 0)
            {
                orderList(all, sortet);
            }
            int i = 0;
            foreach (ProductionPlan pp in sortet)
            {
                i++;
                pp.delete();
                pp._dependence = i;
                ProductionPlanFactory.create(pp);
            }
            
        }

        public static void orderList(List<ProductionPlan> all,List<ProductionPlan> sort)
        {
            if(all.Count == 0){
                return;
            }
            ProductionPlan max = all[0];
            int i = 0;
            int index = 0;
            foreach (ProductionPlan pp in all)
            {
                if (max.getWaitlist() < pp.getWaitlist() && pp._amount > 0)
                {
                    max = pp;
                    index = i;
                }
                i++;
            }

            sort.Add(max);
            all.RemoveAt(index);
        }

        public static void splitPlan()
        {
            WorkSchedule.getArticleToSplit();
            //TODO: Aufträge splitten
        }
    }
}
