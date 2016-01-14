using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Core;

namespace Planning_Tool.Production
{
    class CapacityPlanPos : PlanningPosObject
    {
        private string _capacityPlan;

        public string capacityPlan
        {
            get { return _capacityPlan; }
            set { _capacityPlan = value; }
        }

        private string _capacityPlanPos;

        public string capacityPlanPos
        {
            get { return _capacityPlanPos; }
            set { _capacityPlanPos = value; }
        }

        private int _neededTime;

        public int neededTime
        {
            get { return _neededTime; }
            set { _neededTime = value; }
        }

        private int _neededMakeReady;

        public int neededMakeReady
        {
            get { return _neededMakeReady; }
            set { _neededMakeReady = value; }
        }
    }
}
