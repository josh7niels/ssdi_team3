using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    class FindDoctors : IMessage
    {
        List<string> sendBack = new List<string>();
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        string str = ConfigurationManager.AppSettings.Get("dbConnectionString");
        public FindDoctors()
        {
        }
        public List<string> execute()
        {
            sendBack.Add("04");
            try
            {
                string query = "SELECT concat(first_name,' ',last_name) from user where role='D';";
                con = new MySqlConnection(str);
                con.Open();
                cmd = new MySqlCommand(query, con);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sendBack.Add(reader.GetString(0));

                    }
                }
                reader.Close();
                con.Close();
            }
            catch (MySqlException err)
            {
                throw err;
            }
            return sendBack;
        }
    }
}
