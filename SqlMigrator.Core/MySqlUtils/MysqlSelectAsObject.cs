using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
namespace SqlMigrator.Core.MySqlUtils
{
    public class MysqlSelectAsObject<T> where T : SqlObject, new()
    {
        private readonly MySqlConnection connection;

        public MysqlSelectAsObject( MySqlConnection connection)
        {
            this.connection = connection;
        }

        public List<T> Execute(string command){
            Console.WriteLine(command);
            List<T> all = new List<T>();
            MySqlCommand cmd = new MySqlCommand(command,connection);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    
                    var t = Activator.CreateInstance<T>();
                    all.Add(t);
                    var props = t.GetType().GetProperties();
                    foreach(var prop in props){
                        var attrs = prop.GetCustomAttributes(true);
                        foreach(var attr in attrs){
                            SqlMemberAttribute found = attr as SqlMemberAttribute;
                            if(found != null){
                                string val = reader.GetString(found.Map);
                                prop.SetValue(t,val);
                            }
                        }
                    }
                }
            }
            return all;
        }
    }
}