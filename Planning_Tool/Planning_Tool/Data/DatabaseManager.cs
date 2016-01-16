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
            try
            {
                connection = new SQLiteConnection("Data Source=" + DATABASE);
            }
            finally
            {
                //nichts tun
            }
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
                connection = null;
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
        }

        /// <summary>
        /// Läd zu einer Klasse die gewünschten Daten aus der Datenbank
        /// </summary>
        /// <param name="type">Typ der Klasse</param>
        /// <param name="where">SQL where wenn beschtimmte bedingungen gelten sollen</param>
        /// <returns>List<Object> mit den Gefunden Objecten</returns>
        public List<Object> freeSelect(Type type, string sql)
        {
            List<Object> res = new List<object>();
            PropertyInfo[] properties;

            if (!open)
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
        }

        /// <summary>
        /// Baut die Datenbank für sämtliche Klassen die Datenbanktabellen auf
        /// </summary>
        public void initialize()
        {
            string sql;
            string fields;
            string primary;
            bool hasDependence = false;
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
                hasDependence = false;
                sql = "CREATE TABLE IF NOT EXISTS " + t.Name + " (";

                prop = t.GetProperties();
                int anz = 1;
                fields = null;
                foreach(PropertyInfo p in prop) 
                {
                    if (p.Name.Equals("dependence",StringComparison.CurrentCultureIgnoreCase)){
                        hasDependence = true;
                    }
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
                    primary += ", " + t.Name.Remove(t.Name.Length-3);

                    if (hasDependence)
                    {
                        primary += ", dependence";
                    }

                    primary += ")";
                }
                else
                {
                    if (hasDependence)
                    {
                        primary += ", dependence";
                    }
                    primary += ")";
                }

                sql += primary;
                sql += ")";

                command.CommandText = sql;
                command.ExecuteNonQuery();

            }

            if (Properties.isTestmode)
            {
                FillTable.createMasterdata();
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

            if (Properties.isTestmode)
            {
                tables = Properties.classes;
            }
            else
            {
                tables = Properties.deleteTables;
            }
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
            string sql,where,valuePos = null,valueHead = null,valueDependence = null;
            string fields;
            string table = obj.GetType().Name;
            bool isPos = table.EndsWith("Pos",StringComparison.CurrentCultureIgnoreCase);
            bool hasDependence = false;
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
                        valuePos = p.GetValue(obj).ToString();
                    }
                    else if (isPos)
                    {
                        if (p.Name.Equals(table.Remove(table.Length - 3), StringComparison.CurrentCultureIgnoreCase))
                        {
                            valueHead = p.GetValue(obj).ToString();
                        }
                    }

                    if (p.Name.Equals("dependence", StringComparison.CurrentCultureIgnoreCase))
                    {
                        hasDependence = true;
                        valueDependence = p.GetValue(obj).ToString();
                    }

                    if (p.PropertyType.Equals(typeof(bool)))
                    {
                        if (p.GetValue(obj).Equals(true))
                        {
                            fields += p.Name + " = " + "\"" + 1 + "\"";
                        }
                        else
                        {
                            fields += p.Name + " = " + "\"" + 2 + "\"";
                        }
                    }
                    else
                    {
                        fields += p.Name + " = " + "\"" + p.GetValue(obj) + "\"";
                    }
                }
                anz++;
            }

            if (fields == null)
            {
                return;
            }

            where = "WHERE " + table + " = \"" + valuePos + "\"";
            if (isPos)
            {
                where += " AND " + table.Remove(table.Length - 3) + " = \"" + valueHead + "\""; 
            }

            if (hasDependence)
            {
                where += " AND dependence = \"" + valueDependence + "\"";
            }

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

        /// <summary>
        /// Löscht Datensätze aus der Datenbank
        /// </summary>
        /// <param name="table">Name der Tabelle</param>
        /// <param name="where">Where bedingung</param>
        public void delete(string table,string where)
        {
            string sql;

            if (!open)
            {
                connection.Open();
                open = true;
            }

            command = new SQLiteCommand(connection);

            sql = "DELETE FROM " + table;
            sql += where;

            command.CommandText = sql;
            command.ExecuteNonQuery();

        }

        /// <summary>
        /// Löscht ein Object aus der Datenbank
        /// </summary>
        /// <param name="obj"></param>
        public void delete(Object obj)
        {
            Type type;
            PropertyInfo[] prop;
            string sql,secondValue = null,firstValue = null;
            string headTable = null;
            bool isPos = false;

            if (!open)
            {
                connection.Open();
                open = true;
            }

            if (obj == null)
            {
                return;
            }

            type = obj.GetType();
            //prop = type.GetProperty(type.Name);
            prop = obj.GetType().GetProperties();

            if (type.Name.EndsWith("pos", StringComparison.CurrentCultureIgnoreCase))
            {
                isPos = true;
                headTable = type.Name.Remove(type.Name.Length - 3);
            }

            command = new SQLiteCommand(connection);

            foreach (PropertyInfo p in prop)
            {
                //Sich das Suchkriterium merken
                if (p.Name.Equals(type.Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    firstValue = p.GetValue(obj).ToString();
                    if (!isPos)
                    {
                        break;
                    }
                }
                else if (isPos)
                {
                    if (p.Name.Equals(type.Name.Remove(type.Name.Length - 3), StringComparison.CurrentCultureIgnoreCase))
                    {
                        secondValue = p.GetValue(obj).ToString();
                    }
                        
                }
            }

            if (firstValue == null)
            {
                return;
            }

            sql = "DELETE FROM " + type.Name
                + " WHERE " + type.Name + " = \"" + firstValue + "\"";

            if (isPos)
            {
                sql += "AND " + headTable + " = \"" + secondValue + "\"";
            }

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
