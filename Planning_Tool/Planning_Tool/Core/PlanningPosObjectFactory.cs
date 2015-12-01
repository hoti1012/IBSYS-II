using Planning_Tool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Planning_Tool.Exceptions;

namespace Planning_Tool.Core
{
    public abstract class PlanningPosObjectFactory
    {
        /// <summary>
        /// Speichert die änderungen an einer Objectposition in der Datenbank
        /// </summary>
        /// <param name="obj">Objectposition</param>
        public static void update(PlanningPosObject obj)
        {
            DatabaseManager manager;
            manager = new DatabaseManager();
            try
            {
                manager.update(obj);
            }
            finally
            {
                manager.release();
            }
        }

        /// <summary>
        /// Löscht eine ObjectPosition aus der Datenbank
        /// </summary>
        /// <param name="obj">Objectposition</param>
        public static void delete(PlanningPosObject obj)
        {
            DatabaseManager manager;
            manager = new DatabaseManager();
            try
            {
                manager.delete(obj);
            }
            finally
            {
                manager.release();
            }
        }

        /// <summary>
        /// Sucht nach einer Objectposition in der Datenbank
        /// </summary>
        /// <param name="type">Typ des gesuchten objects</param>
        /// <param name="head">nummer des Kopfobjects</param>
        /// <param name="pos">nummer der Position</param>
        /// <returns></returns>
        public static PlanningPosObject search(Type type,string head,string pos)
        {
            DatabaseManager manager;
            string where;
            PlanningPosObject obj = null;
            List<Object> res;

            manager = new DatabaseManager();
            try
            {
                where = "WHERE " + type.Name.Remove(type.Name.Length - 3) + " = \"" + head + "\""
                      + " AND " + type.Name + " = \"" + pos + "\"";

                res = manager.select(type, where);

                if (res != null && res.Count > 0)
                {
                    obj = res[0] as PlanningPosObject;
                }
            }
            finally
            {
                manager.release();
            }
            return obj;
        }

        /// <summary>
        /// Sucht alle Objectposition zu einem Kopfobject in der Datenbank
        /// </summary>
        /// <param name="type">Typ des gesuchten objects</param>
        /// <param name="head">nummer des Kopfobjects</param>
        /// <returns></returns>
        public static List<PlanningPosObject> search(Type type, string head)
        {
            DatabaseManager manager;
            string where;
            List<Object> res;
            List<PlanningPosObject> obj = new List<PlanningPosObject>();

            manager = new DatabaseManager();
            try
            {
                where = "WHERE " + type.Name.Remove(type.Name.Length - 3) + " = \"" + head + "\"";

                res = manager.select(type, where);

                foreach (Object o in res)
                {
                    obj.Add(o as PlanningPosObject);
                }
            }
            finally
            {
                manager.release();
            }
            return obj;
        }


        /// <summary>
        /// Erzeugt eine Objectposition
        /// </summary>
        /// <param name="type">Type der Objectposition</param>
        /// <param name="head">Nummer des Kopfobjects</param>
        /// <param name="pos">Nummer der Position</param>
        /// <returns>Die erzeugte Objectposition</returns>
        public static PlanningPosObject create(Type type, string head, string pos)
        {
            DatabaseManager manager;
            PlanningPosObject obj = null;
            PropertyInfo[] props;

            manager = new DatabaseManager();
            try
            {
                //Prüfen ob es die Objectposition bereits gibt
                obj = search(type, head, pos);
                if(obj != null)
                {
                    return null;
                }

                obj = Activator.CreateInstance(type) as PlanningPosObject;
                props = obj.GetType().GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if (p.Name.Equals(type.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        p.SetValue(obj, pos);
                    }

                    if (p.Name.Equals(type.Name.Remove(type.Name.Length -3), StringComparison.CurrentCultureIgnoreCase))
                    {
                        p.SetValue(obj, head);
                    }
                }
                manager.insert(obj);

            }
            finally
            {
                manager.release();
            }
            return obj;
        }

        internal static PlanningObject getHead(PlanningPosObject obj,Type headType)
        {
            PlanningObject head = null;
            DatabaseManager manager;
            string where;
            List<Object> res;
            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();
            string table = type.Name.Remove(type.Name.Length - 3);
            string value = null;

            manager = new DatabaseManager();
            try
            {
                foreach (PropertyInfo p in props)
                {
                    if (p.Name.Equals(table, StringComparison.CurrentCultureIgnoreCase))
                    {
                        value = p.GetValue(obj).ToString();
                    }
                }
                where = "WHERE " + table + " = \"" + value + "\"";
                res = manager.select(headType, where);
                if (res != null && res.Count > 0)
                    head = res[0] as PlanningObject;
            }
            finally
            {
                manager.release();
            }
            return head;
        }
    }
}
