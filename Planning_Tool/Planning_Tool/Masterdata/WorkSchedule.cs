﻿using System;
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
        private string _workSchedule;

        public string workSchedule
        {
            get { return _workSchedule; }
            set { _workSchedule = value; }
        }
    }
}
