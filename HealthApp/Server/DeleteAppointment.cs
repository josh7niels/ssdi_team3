using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    class DeleteAppointment : IMessage
    {
        string apptID;
        List<string> sendBack = new List<string>();
        List<string> databaseResponse = new List<string>();
        public DeleteAppointment(string id)
        {
            apptID = id;
        }
        public List<string> execute()
        {
            string query = "delete from appointmentbookingdetail where appointment_id = @appointment_id ;";
            MySqlCommand a = new MySqlCommand(query);
            a.Parameters.AddWithValue("@appointment_id", apptID);
            databaseCommunicator myDB = new databaseCommunicator(a, "03");
            databaseResponse = myDB.executeQuery();
            sendBack.Add("03");
            sendBack.Add(databaseResponse[0]);
            
            return sendBack;
        }
    }
}
