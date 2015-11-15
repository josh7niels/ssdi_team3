using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class LoginValidation : IMessage
    {
        List<string> sendBack = new List<string>();
        string username, password;
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        string str = ConfigurationManager.AppSettings.Get("dbConnectionString");
        public LoginValidation(string un, string pass)
        {
            username = un;
            password = pass;
        }
        public List<string> execute()
        {
            sendBack.Add("01");
            string query = "Select concat(first_name,' ',last_name) from user u join login l on u.u_id=l.u_id where l.u_id=@u_id and binary l.password = @password;";
            con = new MySqlConnection(str);
            con.Open();  //open the connection
            cmd = new MySqlCommand(query, con);
            cmd.Prepare();
            try
            {
                //we will bound a value to the placeholder
                cmd.Parameters.AddWithValue("@u_id", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
            }
            catch (Exception err)
            {
                Console.WriteLine("Error: " + err.ToString());
            }

            if (reader.Read())
            {
                sendBack.Add("1");
                sendBack.Add(reader.GetString(0));
            }
            else
                sendBack.Add("0");

            con.Close();
            reader.Close();
            return sendBack;
        }
    }
}
