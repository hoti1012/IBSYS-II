using Planning_Tool.Core;
using Planning_Tool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Time
{
    class PeriodFactory : PlanningObjectFactory
    {
        public static int getCurrent()
        {
            DatabaseManager manager;
            string where = null;
            Period pObj = null;
            List<Object> objects = null;
            int max = 1;
            int tmp = 0;

            manager = new DatabaseManager();
            try
            {
                where = "";
                objects = manager.select(typeof(Period),where);
                foreach (Object o in objects)
                {
                    pObj = o as Period;
                    tmp = Convert.ToInt32(pObj.period);
                    if (tmp > max)
                    {
                        max = tmp;
                    }

                }
            }
            finally
            {
                manager.release();
            }
            return max + 1;
        }
    }
}
