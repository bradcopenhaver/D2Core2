using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace Destiny2App.Models
{
    public class DataClass
    {
        private SQLiteConnection sqlite;

        public DataClass()
        {
            sqlite = new SQLiteConnection("Data Source=Database/world_sql_content_6bc14cc5650f6485f655e5028b3cda26.sqlite");
        }

        public string RunQuery(string query)
        {
            SQLiteDataReader rdr;
            string tableData = "";

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    tableData = rdr.GetString(0); //fill the datasource
                }
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
            sqlite.Close();
            return tableData;

        }
    }
}
