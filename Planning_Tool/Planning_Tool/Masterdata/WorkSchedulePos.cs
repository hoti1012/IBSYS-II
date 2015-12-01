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
        /// Nummer des Arbeitsplatzes
        /// </summary>
        private string _workSchedulePos;

        /// <summary>
        /// Rüstzeit
        /// </summary>
        private int _makeReady;

        /// <summary>
        /// Stückarbeitszeit
        /// </summary>
        private int _workTime;

        /// <summary>
        /// Enthält die Positionsnummer des Nächsten Arbeitsgangs
        /// </summary>
        private string _nextPos;

        /// <summary>
        /// Gibt die nächste Position zurück
        /// </summary>
        /// <returns></returns>
        public WorkSchedulePos getNextPos()
        {
            WorkSchedulePos pos = null;
            try
            {
                if (_nextPos != null)
                {
                    pos = WorkSchedulePosFactory.search(typeof(WorkSchedulePos),_workSchedule,_nextPos) as WorkSchedulePos;
                }
            }
            finally
            {

            }
            return pos;
        }

        public string nextPos
        {
            get { return _nextPos; }
            set { _nextPos = value; }
        }

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
