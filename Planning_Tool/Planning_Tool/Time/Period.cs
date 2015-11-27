using Planning_Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Time
{
    class Period : PlanningObject
    {
        private string _period;


        public static int getCurrentPeriod()
        {
            return PeriodFactory.getCurrent();
        }
        public string period
        {
            get { return _period; }
            set { _period = value; }
        }
    }
}
