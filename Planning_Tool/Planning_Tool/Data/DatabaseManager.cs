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
        public List<Object> select(Type type, string where)
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

        /// <summary>
        /// Baut die Datenbank für sämtliche Klassen die Datenbanktabellen auf
        /// </summary>
        public void initialize()
        {
            string sql;
            string fields;
            string primary;
            Type[] classes;
            PropertyInfo[] prop;

            if (!open)
            {
                connection.Open();
                open = true;
            }

            dropTables();

            classes = Properties.classes;
            command = new SQLiteCommand(connection);

            foreach(Type t in classes)
            {
                sql = "CREATE TABLE IF NOT EXISTS " + t.Name + " (";

                prop = t.GetProperties();
                int anz = 1;
                fields = null;
                foreach(PropertyInfo p in prop) 
                {
                    fields += p.Name + " " + p.PropertyType.Name;
                    if(anz < prop.Length)
                    {
                        fields += ", ";
                    }
                    anz++;
                }
                if(fields == null)
                {
                    break;
                }

                sql += fields;
                //Primary Key setzen (bei Pos Tabellen eine Kombination aus Kopf und Pos)
                primary = ", PRIMARY KEY(" + t.Name;
                if (t.Name.EndsWith("pos",StringComparison.CurrentCultureIgnoreCase))
                {
                    primary += ", " + t.Name.Remove(t.Name.Length-3) + ")";
                }
                else
                {
                    primary += ")";
                }

                sql += primary;
                sql += ")";

                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Löscht Tabellen welche nur Temp. Daten enthält
        /// </summary>
        private void dropTables()
        {
            string sql;
            Type[] tables;

            if (!open)
            {
                connection.Open();
                open = true;
            }

            tables = Properties.deleteTables;
            command = new SQLiteCommand(connection);

            foreach (Type t in tables)
            {
                sql = "DROP TABLE IF EXISTS " + t.Name;

                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Führt ein Datenbank update durch
        /// </summary>
        /// <param name="obj">Object welches in der Datenbank aktuallisiert werden soll</param>
        public void update(Object obj)
        {
            string sql,where,value = null;
            string fields;
            string table = obj.GetType().Name;
            PropertyInfo[] prop = obj.GetType().GetProperties();

            if (!open)
            {
                connection.Open();
                open = true;
            }

            command = new SQLiteCommand(connection);

            sql = "UPDATE " + table + " SET ";

            int anz = 1;
            fields = null;
            foreach (PropertyInfo p in prop)
            {
                if (p.GetValue(obj) != null )
                {
                    if(fields != null && anz <= prop.Length)
                        fields += ", ";
                    //Sich das Suchkriterium merken
                    if (p.Name.Equals(table, StringComparison.CurrentCultureIgnoreCase)) 
                    {
                        value = p.GetValue(obj).ToString();
                    }
                    fields += p.Name + " = " + "\"" + p.GetValue(obj) + "\"";
                }
                anz++;
            }

            if (fields == null)
            {
                return;
            }

            where = "WHERE " + table + " = \"" + value + "\""; 

            sql += fields;
            sql += " " + where;

            command.CommandText = sql;
            command.ExecuteNonQuery();

        }

        /// <summary>
        /// Speichert ein Object in die passende Tabelle
        /// </summary>
        /// <param name="obj">Object welches in der Datenbank aktuallisiert werden soll</param>
        public void insert(Object obj)
        {
            string sql, values;
            string fields;
            string table = obj.GetType().Name;
            PropertyInfo[] prop = obj.GetType().GetProperties();

            if (!open)
            {
                connection.Open();
                open = true;
            }

            command = new SQLiteCommand(connection);

            sql = "INSERT INTO " + table;

            int anz = 1;
            fields = null;
            values = null;
            foreach (PropertyInfo p in prop)
            {
                if (p.GetValue(obj) != null)
                {
                    if (fields != null && anz <= prop.Length)
                    {
                        fields += ", ";
                        values += ", ";
                    }

                    fields += p.Name;
                    values += "\"" + p.GetValue(obj) + "\"";
                }
                anz++;
            }

            if (fields == null)
            {
                return;
            }

            sql += " (" + fields + ")";
            sql += " VALUES (" + values + " )";

            command.CommandText = sql;
            command.ExecuteNonQuery();

        }



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
