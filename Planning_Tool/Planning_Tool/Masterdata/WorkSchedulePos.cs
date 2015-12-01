using Planning_Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Masterdata
{
    class WorkSchedulePos : PlanningPosObject
    {
        public static string TABLE = typeof(WorkSchedulePos).Name;

        /// <summary>
        /// Nummer des Arbeitsplanes
        /// </summary>
        private string _workSchedule;

        /// <summary>
        /// Nummer des Arbeitsgangs
        /// </summary>
        private string _workSchedulePos;

        /// <summary>
        /// Nummer des Arbeitsplatzes
        /// </summary>
        private string _workPlace;

        public string workSchedule
        {
            get { return _workSchedule; }
            set { _workSchedule = value; }
        }

        public string workSchedulePos
        {
            get { return _workSchedulePos; }
            set { _workSchedulePos = value; }
        }

        public string workPlace
        {
            get { return _workPlace; }
            set { _workPlace = value; }
        }
    }
}
