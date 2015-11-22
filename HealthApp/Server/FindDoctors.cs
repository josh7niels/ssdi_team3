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
        List<string> databaseResponse = new List<string>();
        public FindDoctors()
        {
        }
        public List<string> execute()
        {
            string query = "SELECT concat(first_name,' ',last_name) from user where role='D';";
            MySqlCommand a = new MySqlCommand(query);
            databaseCommunicator myDB = new databaseCommunicator(a, "04");
            databaseResponse = myDB.executeQuery();
            sendBack.Add("04");
            for (int i = 0; i < databaseResponse.Count; i++)
            {
                sendBack.Add(databaseResponse[i]);
            }
            
            return sendBack;
        }
    }
}
