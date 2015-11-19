using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Reflection;

namespace Planning_Tool.Data
{
    class DatabaseManager
    {
        public static string DATABASE = "database.db";

        private SQLiteConnection connection;

        private SQLiteCommand command;

        private SQLiteDataReader reader;

        private bool open;

        /// <summary>
        /// Baut eine Verbindung mit der Datenbank auf
        /// </summary>
        public DatabaseManager()
        {
            connection = new SQLiteConnection("Data Source=" + DATABASE);
        }

        /// <summary>
        /// Schließt die verbindung und Löscht den cache
        /// </summary>
        public void release() 
        {
            if (open)
            {
                connection.Close();
                connection.Dispose();
                open = false;
            }
            else
                connection.Dispose();
        }//release

        /// <summary>
        /// Läd zu einer Klasse die gewünschten Daten aus der Datenbank
        /// </summary>
        /// <param name="type">Typ der Klasse</param>
        /// <param name="where">SQL where wenn beschtimmte bedingungen gelten sollen</param>
        /// <returns>List<Object> mit den Gefunden Objecten</returns>
        public List<Object> load(Type type, string where)
        {
            List<Object> res = new List<object>();
            string sql = "SELECT * FROM " + type.Name;
            PropertyInfo[] properties;

            if (where != null) 
            {
                sql += " " + where;
            }

            if(!open)
            {
                connection.Open();
                open = true;
            }

            command = new SQLiteCommand(connection);
            command.CommandText = sql;
            reader = command.ExecuteReader();

            int o = 0;
            while (reader.Read())
            {
                res.Add(Activator.CreateInstance(type));
                properties = res[o].GetType().GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    if (reader[properties[i].Name].GetType() != typeof(System.DBNull))

                        properties[i].SetValue(res[o], reader[properties[i].Name]);
                }
                o++;
            }
            return res;
        }//load



        public bool Open
        {
            get { return open; }
            set { open = value; }
        }

        public SQLiteCommand Command
        {
            get { return command; }
            set { command = value; }
        }

        public SQLiteConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public SQLiteDataReader Reader
        {
            get { return reader; }
            set { reader = value; }
        }
    }
}
