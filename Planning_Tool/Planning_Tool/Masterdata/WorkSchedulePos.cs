using Planning_Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Masterdata
{
    public class WorkSchedulePos : PlanningPosObject
    {
        public static string TABLE = typeof(WorkSchedulePos).Name;

        /// <summary>
        /// Nummer des Arbeitsplanes
        /// </summary>
        private string _workSchedule;

        /// <summary>
        /// Nummer des Arbeitsplatzes
        /// </summary>
        private string _workSchedulePos;

        /// <summary>
        /// Gibt die Reihenfolge an in welcher der Arbeitsgang ansteht
        /// </summary>
        private string _dependence;

        /// <summary>
        /// Rüstzeit
        /// </summary>
        private int _makeReady;

        /// <summary>
        /// Stückarbeitszeit
        /// </summary>
        private int _workTime;

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

        public string dependence
        {
            get { return _dependence; }
            set { _dependence = value; }
        }

        public int makeReady
        {
            get { return _makeReady; }
            set { _makeReady = value; }
        }

        public int workTime
        {
            get { return _workTime; }
            set { _workTime = value; }
        }
    }
}
