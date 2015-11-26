using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Masterdata;
using Planning_Tool.Purchase;
using Planning_Tool.Production;

namespace Planning_Tool.Data
{
    class Properties
    {
        /// <summary>
        /// Hier sind alle Klassen eingetragen welche eine Datenbanktabelle haben oder Brauchen
        /// </summary>
        public static Type[] classes = {typeof(Article)
                                       ,typeof(Ordering)
                                       ,typeof(OrderingPos)
                                       ,typeof(BOM)
                                       ,typeof(BOMpos)
                                       ,typeof(WorkSchedule)
                                       ,typeof(WorkSchedulePos)
                                       ,typeof(Workplace)
                                       ,typeof(Stock)
                                       };

        public static Type[] deleteTables = {typeof(Ordering)
                                            ,typeof(OrderingPos)
                                            ,typeof(Stock)
                                            };
    }
}
