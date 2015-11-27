using Planning_Tool.Core;
using Planning_Tool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Masterdata
{
    public class StockFactory : PlanningObjectFactory
    {
        /// <summary>
        /// Liefert alle Lagerplätze
        /// </summary>
        /// <returns></returns>
        public static List<Stock> getAll()
        {
            List<Stock> stock = null;
            List<Object> objects = null;
            DatabaseManager manager;

            manager = new DatabaseManager();
            try
            {
                objects = manager.select(typeof(Stock), "");
                stock = new List<Stock>();
                foreach (Object o in objects)
                {
                    stock.Add(o as Stock);
                }
            }
            finally
            {
                manager.release();
            }
            return stock;
        }
    }
}
