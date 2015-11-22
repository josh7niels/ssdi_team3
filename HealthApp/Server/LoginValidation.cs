using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class LoginValidation : IMessage
    {
        List<string> sendBack = new List<string>();
        List<string> databaseResponse = new List<string>();
        string username, password;
        public LoginValidation(string un, string pass)
        {
            username = un;
            password = pass;
        }
        public List<string> execute()
        {
            string query = "Select concat(first_name,' ',last_name) from user u join login l on u.u_id=l.u_id where l.u_id=@u_id and binary l.password = @password;";
            MySqlCommand a = new MySqlCommand(query);
            a.Parameters.AddWithValue("@u_id", username);
            a.Parameters.AddWithValue("@password", password);
            databaseCommunicator myDB = new databaseCommunicator(a, "01");
            databaseResponse = myDB.executeQuery();
            sendBack.Add("01");
            sendBack.Add(databaseResponse[0]);
            sendBack.Add(databaseResponse[1]);
            
            return sendBack;
        }
    }
}
