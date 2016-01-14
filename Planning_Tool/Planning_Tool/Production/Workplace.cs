using Planning_Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Production
{
    public class Workplace : PlanningObject
    {
        public static string TABLE = typeof(Workplace).Name;

        /// <summary>
        /// Nummer des Arbeitsplatzes
        /// </summary>
        private string _workplace;

        public WorkplacePos addPos(string article)
        {
            WorkplacePos pos = null;
            pos = WorkplacePosFactory.create(typeof(WorkplacePos), this._workplace, article) as WorkplacePos;
            return pos;
        }

        public string workplace
        {
            get { return _workplace; }
            set { _workplace = value; }
        }

    }
}
