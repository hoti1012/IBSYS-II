using Planning_Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Masterdata
{
    class WorkSchedule : PlanningObject
    {
        public static string TABLE = typeof(WorkSchedule).Name;

        /// <summary>
        /// Arbeitsgang Nummer
        /// </summary>
        private string _workSchedule;

        public string workSchedule
        {
            get { return _workSchedule; }
            set { _workSchedule = value; }
        }
    }
}
