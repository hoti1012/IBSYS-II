using Planning_Tool.Core;
using Planning_Tool.Data;
using Planning_Tool.Exceptions;
using Planning_Tool.Masterdata;
using Planning_Tool.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Purchase
{
    class OrderingPosFactory : PlanningPosObjectFactory
    {

        public static List<OrderingPos> getAllCurrentOrder()
        {
            string sql = "SELECT * FROM OrderingPos WHERE Ordering = " + Period.getCurrentPeriod();
            List<OrderingPos> res = new List<OrderingPos>();
            foreach(OrderingPos pos in OrderingPosFactory.select(typeof(OrderingPos),sql))
            {
                res.Add(pos);
            }
            return res;
        }
    }
}
