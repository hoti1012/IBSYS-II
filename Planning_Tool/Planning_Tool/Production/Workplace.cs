using Planning_Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Production
{
    class Workplace : PlanningObject
    {
        public static string TABLE = typeof(Workplace).Name;

        /// <summary>
        /// Nummer des Arbeitsplatzes
        /// </summary>
        private string _workplace;

        public string workplace
        {
            get { return _workplace; }
            set { _workplace = value; }
        }

    }
}
