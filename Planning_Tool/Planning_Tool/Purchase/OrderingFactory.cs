using Planning_Tool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Purchase
{
    class OrderingFactory
    {
        /// <summary>
        /// Erzeugt einen Bestellkopf
        /// </summary>
        /// <param name="ordering"></param>
        /// <returns></returns>
        public static Ordering create(string ordering)
        {
            if (ordering == null)
                return null;

            Ordering orderingObj = new Ordering();
            orderingObj.ordering = ordering;
            DatabaseManager manager = new DatabaseManager();
            try
            {
                if (OrderingFactory.serach(ordering) != null)
                {
                    //fehler werfen
                }
                manager.insert(orderingObj);
            }
            finally
            {
                manager.release();
            }

            return orderingObj;
        }

        /// <summary>
        /// Speichert die änderungen an einem Objekt in der Datenbank
        /// </summary>
        /// <param name="orderingObj">Object welches gespeichert werden soll</param>
        public static void update(Ordering orderingObj)
        {
            DatabaseManager manager;
            manager = new DatabaseManager();
            try
            {
                manager.update(orderingObj);
            }
            finally
            {
                manager.release();
            }
        }

        /// <summary>
        /// Sucht eine Bestellung aus der Datenbank
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Gesuchte Bestellung</returns>
        public static Ordering serach(string order)
        {
            if (order == null)
            {
                return null;
            }
            DatabaseManager manager;
            Ordering orderingObj = null;
            List<Object> res = new List<object>();
            string where;

            manager = new DatabaseManager();
            try
            {
                where = "WHERE " + orderingObj.GetType().Name + " = " + order;
                res = manager.select(typeof(Ordering), where);
                if (res != null && res.Count > 0)
                {
                    orderingObj = res[0] as Ordering;
                }
            }
            finally
            {
                manager.release();
            }
            return orderingObj;
        }
    }
}
