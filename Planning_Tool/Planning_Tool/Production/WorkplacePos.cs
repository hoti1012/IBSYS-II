using Planning_Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Production
{
    public class WorkplacePos : PlanningPosObject
    {
        public static string TABLE = typeof(WorkplacePos).Name;

        private string _workplace;

        private string _workplacePos;

        private int _amountWaitlist;

        private int _amountInWork;

        public string workplace
        {
            get { return _workplace; }
            set { _workplace = value; }
        }

        public string workplacePos
        {
            get { return _workplacePos; }
            set { _workplacePos = value; }
        }

        public int amountInWork
        {
            get { return _amountInWork; }
            set { _amountInWork = value; }
        }

        public int amountWaitlist
        {
            get { return _amountWaitlist; }
            set { _amountWaitlist = value; }
        }


    }
}
