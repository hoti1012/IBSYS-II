using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Core;
using Planning_Tool.Masterdata;

namespace Planning_Tool.Production
{
    class WaitingListPlan : PlanningObject
    {
        private string _waitingListPlan;

        public string waitingListPlan
        {
            get { return _waitingListPlan; }
            set { _waitingListPlan = value; }
        }

        private int _neededTime;

        public int neededTime
        {
            get { return _neededTime; }
            set { _neededTime = value; }
        }

        /// <summary>
        /// Berechnet für alle Arbeitsplätze die Wartelisten und die inArbeit
        /// </summary>
        public static void createAllWaitinglistPlan()
        {
            foreach(WorkplacePos workPos in WorkplacePosFactory.search(typeof(WorkplacePos)))
            {
                WorkSchedulePos wspos = WorkSchedulePosFactory.search(typeof(WorkSchedulePos),workPos.workplacePos,workPos.workplace) as WorkSchedulePos;
                WaitingListPlan waiting = WaitingListPlanFactory.create(typeof(WaitingListPlan), workPos.workplace) as WaitingListPlan;
                if (waiting == null)
                {
                    waiting = WaitingListPlanFactory.search(typeof(WaitingListPlan), workPos.workplace) as WaitingListPlan;
                }
                waiting._neededTime += workPos.amountInWork * wspos.workTime + workPos.amountWaitlist * wspos.workTime;
                waiting.update();

                foreach (WorkSchedulePos wspos2 in wspos.getAllPosAfterThis())
                {
                    WaitingListPlan waiting2 = WaitingListPlanFactory.create(typeof(WaitingListPlan), wspos2.workSchedulePos) as WaitingListPlan;
                    if (waiting2 == null)
                    {
                        waiting2 = WaitingListPlanFactory.search(typeof(WaitingListPlan), wspos2.workSchedulePos) as WaitingListPlan;
                    }
                    waiting2._neededTime += workPos.amountInWork * wspos2.workTime + workPos.amountWaitlist * wspos2.workTime;
                    waiting2.update();
                }
            }


        }
    }
}
