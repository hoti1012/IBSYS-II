using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Masterdata;
using Planning_Tool.Purchase;
using Planning_Tool.Production;
using Planning_Tool.Time;
using Planning_Tool.Forecasts;

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
                                       ,typeof(Period)
                                       ,typeof(Workplace)
                                       ,typeof(WorkplacePos)
                                       ,typeof(Forecast)
                                       ,typeof(ProductionPlan)
                                       ,typeof(DirektSale)
                                       ,typeof(OrderBOM)
                                       ,typeof(OrderBOMpos)
                                       };

        public static Type[] deleteTables = {typeof(Ordering)
                                            ,typeof(OrderingPos)
                                            ,typeof(Stock)
                                            ,typeof(WorkplacePos)
                                            ,typeof(Forecast)
                                            ,typeof(ProductionPlan)
                                            ,typeof(DirektSale)
                                            ,typeof(OrderBOM)
                                            ,typeof(OrderBOMpos)
                                            };

        //TODO: Bei fertigstellung auf false setzen
        public static bool isTestmode = true;
    }
}
