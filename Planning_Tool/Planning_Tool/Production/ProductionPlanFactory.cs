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

        public static List<ProductionPlan> getOrderdProductionPlan()
        {
            string sql = "SELECT * FROM ProductionPlan Order By dependence";
            List<ProductionPlan> res = new List<ProductionPlan>();
            foreach(ProductionPlan pp in ProductionPlanFactory.select(typeof(ProductionPlan),sql))
            {
                res.Add(pp);
            }
            return res;
        }
    }
}
