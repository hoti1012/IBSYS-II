﻿using Planning_Tool.Data;
using Planning_Tool.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Planning_Tool.Core
{
    public abstract class PlanningObjectFactory
    {
        /// <summary>
        /// Führt ein Datenbankupdate für das übergebene Object aus
        /// </summary>
        /// <param name="planningObject"></param>
        internal static void update(PlanningObject planningObject)
        {
            DatabaseManager manager;
            manager = new DatabaseManager();
            try
            {
                manager.update(planningObject);
            }
            finally
            {
                manager.release();
            }
        }

        /// <summary>
        /// Löscht ein Object aus der Datenbank
        /// </summary>
        /// <param name="planningObject"></param>
        internal static void delete(PlanningObject planningObject)
        {
            DatabaseManager manager;
            manager = new DatabaseManager();
            try
            {
                manager.delete(planningObject);
            }
            finally
            {
                manager.release();
            }
        }

        /// <summary>
        /// Sucht nach einem Object in der Datenbank
        /// </summary>
        /// <param name="type">Typ des gesuchten objects</param>
        /// <param name="head">nummer des Kopfobjects</param>
        /// <returns>Das gesuchte Object</returns>
        public static PlanningObject search(Type type, string head)
        {
            DatabaseManager manager;
            string where;
            PlanningObject obj = null;
            List<Object> res;

            manager = new DatabaseManager();
            try
            {
                where = "WHERE " + type.Name + " = \"" + head + "\"";

                res = manager.select(type, where);

                if (res != null && res.Count > 0)
                {
                    obj = res[0] as PlanningObject;
                }
            }
            finally
            {
                manager.release();
            }
            return obj;
        }

        /// <summary>
        /// Sucht nach einem Object in der Datenbank
        /// </summary>
        /// <param name="type">Typ des gesuchten objects</param>
        /// <param name="sql">nummer des Kopfobjects</param>
        /// <returns>Das gesuchte Object</returns>
        public static List<PlanningObject> select(Type type, string sql)
        {
            DatabaseManager manager;
            List<Object> res;
            List<PlanningObject> obj = new List<PlanningObject>();

            manager = new DatabaseManager();
            try
            {

                res = manager.freeSelect(type, sql);

                foreach(Object o in res)
                {
                    obj.Add(o as PlanningObject);
                }
            }
            finally
            {
                manager.release();
            }
            return obj;
        }

        /// <summary>
        /// Sucht nach einem Object in der Datenbank
        /// </summary>
        /// <param name="type">Typ des gesuchten objects</param>
        /// <returns>Alle Objecte</returns>
        public static List<PlanningObject> searchAll(Type type)
        {
            DatabaseManager manager;
            string where;
            List<PlanningObject> obj = new List<PlanningObject>();
            List<Object> res;

            manager = new DatabaseManager();
            try
            {
                res = manager.select(type,null);

                foreach(Object o in res)
                {
                    obj.Add(o as PlanningObject);
                }
            }
            finally
            {
                manager.release();
            }
            return obj;
        }

        /// <summary>
        /// Gibt alle Positionen zu diesem Kopfobject zurück
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="head">Nummer des Kopfobjects</param>
        /// <returns>Eine Liste mit allen Positionsobjecten</returns>
        public static List<PlanningPosObject> searchAllPos(Type type,string head)
        {
            return PlanningPosObjectFactory.search(type, head);
        }

        /// <summary>
        /// Erzeugt ein Object
        /// </summary>
        /// <param name="type">Type des Objects</param>
        /// <param name="head">Nummer des Objects</param>
        /// <returns>Das erzeugte Object</returns>
        public static PlanningObject create(Type type, string head)
        {
            DatabaseManager manager;
            PlanningObject obj = null;
            PropertyInfo[] props;

            manager = new DatabaseManager();
            try
            {
                //Prüfen ob es die Objectposition bereits gibt
                obj = search(type, head);
                if (obj != null)
                {
                    return null;
                }

                obj = Activator.CreateInstance(type) as PlanningObject;
                props = obj.GetType().GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if (p.Name.Equals(type.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        p.SetValue(obj, head);
                        break;
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
    }
}
