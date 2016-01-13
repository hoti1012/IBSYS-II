using Planning_Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Masterdata
{
    public class WorkSchedule : PlanningObject
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

        /// <summary>
        /// Erzeugt einen Arbeitsgang
        /// </summary>
        /// <param name="pos">Arbeitsplatz</param>
        /// <returns>Den erzeugten Arbeitsgang</returns>
        public WorkSchedulePos addWorkSchedulePos(string pos)
        {
            int i = WorkSchedulePosFactory.search(typeof(WorkSchedulePos),_workSchedule).Count + 1;
            return WorkSchedulePosFactory.create(typeof(WorkSchedulePos),_workSchedule,pos,i.ToString()) as WorkSchedulePos;
        }
    }
}
