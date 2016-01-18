using Planning_Tool.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Masterdata;

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


        internal static List<Workplace> getMostUsedWorkplace()
        {
            List<Workplace> res = new List<Workplace>();
            List<CountObject> list = new List<CountObject>();
            foreach (Workplace wp in WorkplaceFactory.searchAll(typeof(Workplace)))
            {
                string sql = "SELECT * FROM WorkSchedulePos WHERE WorkSchedulePos = " + wp.workplace;
                CountObject co = new CountObject(wp._workplace, WorkSchedulePosFactory.select(typeof(WorkSchedulePos), sql).Count);
                list.Add(co);
            }
            double interest = 0;
            foreach (CountObject co in list)
            {
                interest += co.count;
            }
            interest = interest / list.Count;

            list.Sort();

            foreach (CountObject co in list)
            {
                if (co.count > interest)
                {

                }
            }

           return res;
        }
    }
}
