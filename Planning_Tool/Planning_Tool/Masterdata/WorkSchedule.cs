using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Masterdata
{
    class WorkSchedule
    {
        public static string TABLE = "WORKSCHEDULE";

        /// <summary>
        /// Arbeitsgang Nummer
        /// </summary>
        private string _workSchudle;

        public string WorkSchudle
        {
            get { return _workSchudle; }
            set { _workSchudle = value; }
        }
    }
}
