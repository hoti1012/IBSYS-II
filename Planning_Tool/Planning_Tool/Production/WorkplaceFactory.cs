using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Data;
using Planning_Tool.Exceptions;
using Planning_Tool.Core;

namespace Planning_Tool.Production
{
    public class WorkplaceFactory : PlanningObjectFactory
    {
        public static List<Workplace> getAll()
        {
            List<Workplace> res = new List<Workplace>();
            foreach(Workplace wp in WorkplaceFactory.searchAll(typeof(Workplace)))
            {
                res.Add(wp);
            }
            return res;
        }
    }
}
