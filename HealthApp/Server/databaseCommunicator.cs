using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Server
{
    public class databaseCommunicator : IDBConnect
    {
        string type, conStr = ConfigurationManager.AppSettings.Get("dbConnectionString");
        List<string> sendBack = new List<string>();
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        public databaseCommunicator()
        {
        }
        public void setValues(MySqlCommand c, string t)
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
                switch (type)
                {
                    case "01"://type could be "getOneValue"
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            sendBack.Add("1");
                            sendBack.Add(reader.GetString(0));
                        }
                        else
                        {
                            sendBack.Add("0");
                            sendBack.Add("null");
                        }
                        break;
                    case "02"://type could be "getAllMultiplePerEntry"
                        reader = cmd.ExecuteReader();
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
                    case "03"://type could be "insert"
                        count = cmd.ExecuteNonQuery();
                        if (count == 1)
                            sendBack.Add("1");
                        else
                            sendBack.Add("0");
                        break;
                    case "04"://type could be "getAll"
                    case "05":
                    case "06":
                    case "07":
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                                sendBack.Add(reader.GetString(0));
                        break;
                    case "08":
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string appts = reader.GetString(0) + "," + reader.GetString(1) + "," + reader.GetString(2) + "," + reader.GetString(3);
                                sendBack.Add(appts);
                            }
                        }
                        break;
                    case "09":
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string appts = reader.GetString(0) + "," + reader.GetString(1) + "," + reader.GetString(2) + "," + reader.GetString(3);
                                sendBack.Add(appts);
                            }
                        }
                        break;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Error: " + err.ToString());
            }

            dbConnection.Close();
            if (reader != null)
                reader.Close();
            cmd.Dispose();
            return sendBack;
        }
    }
}
