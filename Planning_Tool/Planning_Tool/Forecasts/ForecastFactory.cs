using Planning_Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Forecasts
{
    class ForecastFactory : PlanningObjectFactory
    {
        public static List<Forecast> getAll()
        {
            List<Forecast> res = new List<Forecast>();
            List<PlanningObject> tmp;
            tmp = PlanningObjectFactory.searchAll(typeof(Forecast));
            foreach(PlanningObject p in tmp)
            {
                res.Add(p as Forecast);
            }
            return res;
        }
    }
}
