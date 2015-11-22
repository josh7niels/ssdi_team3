using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    class FindAppointments : IMessage
    {
        string username;
        List<string> sendBack = new List<string>();
        List<string> databaseResponse = new List<string>();
        IDBConnect dbConnector = null;
        public FindAppointments(string un)
        {
            username = un;
        }
        public void setDBConnectInstance(IDBConnect db)
        {
            dbConnector = db;
        }
        public List<string> execute()
        {
            string query = "select appointment_id,physician_id,date,time,cause from appointmentbookingdetail where patient_id=@patient_id order by appointment_id desc;";
            MySqlCommand a = new MySqlCommand(query);
            a.Parameters.AddWithValue("@patient_id", username);
            dbConnector.setValues(a, "02");
            databaseResponse = dbConnector.executeQuery();
            sendBack.Add("02");
            for(int i=0; i<databaseResponse.Count; i++)
            {
                sendBack.Add(databaseResponse[i]);
            }

            return sendBack;
        }
    }
}
