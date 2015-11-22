using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Server
{
    class databaseCommunicator : IDBConnect
    {
        string type, conStr = ConfigurationManager.AppSettings.Get("dbConnectionString");
        List<string> sendBack = new List<string>();
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        public databaseCommunicator(MySqlCommand c, string t)
        {
            cmd = c;
            type = t;
        }
        public List<string> executeQuery()
        {
            MySqlConnection dbConnection = new MySqlConnection(conStr);
            dbConnection.Open();  //open the connection
            cmd.Connection = dbConnection;
            cmd.Prepare();
            int count = 0;
            try
            {
                count = cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
            }
            catch (Exception err)
            {
                Console.WriteLine("Error: " + err.ToString());
            }
            switch(type)
            {
                case "01":
                        if (reader.Read())
                        {
                            sendBack.Add("1");
                            sendBack.Add(reader.GetString(0));
                        }
                        else
                            sendBack.Add("0");
                    break;
                case "02":
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string temp = reader.GetString(2).Split(' ').First();
                                string appts = reader.GetString(0) + "," + reader.GetString(1) + "," + temp + "," + reader.GetString(3) + "," + reader.GetString(4);
                                sendBack.Add(appts);
                            }
                        }
                    break;
                case "03":
                        if (count == 1)
                            sendBack.Add("1");
                        else
                            sendBack.Add("0");
                    break;
                case "04":
                case "05":
                case "06":
                        if (reader.HasRows)
                            while (reader.Read())
                                sendBack.Add(reader.GetString(0));
                    break;
            }

            dbConnection.Close();
            reader.Close();
            return sendBack;
        }
    }
}
