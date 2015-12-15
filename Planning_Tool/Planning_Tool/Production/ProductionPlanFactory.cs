using Planning_Tool.Core;
using Planning_Tool.Masterdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Production
{
    class ProductionPlanFactory : PlanningObjectFactory
    {
        /// <summary>
        /// Gibt die Produktionspläne welche zur Baugruppe gehört
        /// </summary>
        /// <param name="productionPlan"></param>
        /// <returns></returns>
        public static List<ProductionPlan> getProductionPlansFromBom(string productionPlan)
        {
            if (productionPlan == null)
            {
                return null;
            }
            List<ProductionPlan> res = new List<ProductionPlan>();
            Article artObj = ArticleFactory.search(typeof(Article),productionPlan) as Article;
            if (artObj != null)
            {
                foreach (BOMpos pos in artObj.getModule())
                {
                    //Zur sicherheit Prüfen ob die BOMPos auch wirklich eine Baugruppe ist
                    if (pos.isModule())
                    {
                        ProductionPlan pp = ProductionPlanFactory.search(typeof(ProductionPlan), pos.bompos) as ProductionPlan;
                        res.Add(pp);
                    }
                }
            }
            return res;
        }
    }
}
