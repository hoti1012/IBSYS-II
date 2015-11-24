using Planning_Tool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Purchase
{
    class OrderingPosFactory
    {
        /// <summary>
        /// Sucht eine Bestellposition
        /// </summary>
        /// <param name="order">Bestellnr</param>
        /// <param name="pos">Positionsnummer</param>
        /// <returns>Gefundene Bestellposition</returns>
        public static OrderingPos search(string order,string pos){
            OrderingPos orderPosObj = null;
            string where;
            List<Object> res = null;
            DatabaseManager manager;

            manager = new DatabaseManager();
            try
            {
                where = "WHERE " + Ordering.TABLE + " = " + order
                        + " AND " + OrderingPos.TABLE + " = " + pos ;

                res = manager.select(typeof(OrderingPos),where);
                if (res != null && res.Count > 0)
                {
                    orderPosObj = res[0] as OrderingPos;
                }
                    
            }
            finally
            {
                manager.release();
            }

            return orderPosObj;
        }

        /// <summary>
        /// Sucht alle Positionen zu einer Bestellung
        /// </summary>
        /// <param name="order">Bestellnummer</param>
        /// <returns>Alle bestellpositionen in einer Bestellung</returns>
        public static List<OrderingPos> serach(string order)
        {
            List<OrderingPos> orderPos = null;
            List<Object> tmp = null;
            DatabaseManager manager;
            string where;
            manager = new DatabaseManager();
            try
            {
                where = "WHERE " + Ordering.TABLE + " = " + order;
                tmp = manager.select(typeof(OrderingPos),where);
                foreach (Object o in tmp) 
                {
                    orderPos.Add(o as OrderingPos);
                }
            }
            finally
            {
                manager.release();
            }
            return orderPos;
        }

        /// <summary>
        /// Erzeugt eine Bestellposition
        /// </summary>
        /// <param name="order"></param>
        /// <param name="pos"></param>
        public static void create(string order,string pos)
        {
            DatabaseManager manager;
            OrderingPos posObj = null;

            manager = new DatabaseManager();
            try
            {
                posObj = search(order, pos);
                if (posObj != null)
                {
                    //TODO: Fehler werfen
                }
                posObj = new OrderingPos();
                posObj.ordering = order;
                posObj.orderingpos = pos;
                manager.insert(posObj);
            }
            finally
            {
                manager.release();
            }
        }

        /// <summary>
        /// Speichert die änderung am Object in der Datenbank
        /// </summary>
        /// <param name="orderPosObj">Bestellposition</param>
        public static void update(OrderingPos orderPosObj)
        {
            DatabaseManager manager;

            manager = new DatabaseManager();
            try
            {
                manager.update(orderPosObj);
            }
            finally
            {
                manager.release();
            }
        }
    }
}
