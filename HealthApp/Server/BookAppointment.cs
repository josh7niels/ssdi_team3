using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    public class BookAppointment : IMessage
    {
        string doctor, patient, apptDate, apptTime;
        List<string> sendBack = new List<string>();
        List<string> databaseResponse = new List<string>();
        IDBConnect dbConnector = null;
        public BookAppointment(string doc, string pat, string date, string time)
        {
            patient = pat;
            doctor = doc;
            apptDate = date;
            apptTime = time;
        }
        public void setDBConnectInstance(IDBConnect db)
        {
            dbConnector = db;
        }
        private void convertDate()
        {
            string[] a;
            a = apptDate.Split('/');
            apptDate = a[2] + "-" + a[0] + "-" + a[1];
        }
        public List<string> execute()
        {
            convertDate();
            string query = "Select u_id from user where concat(first_name, ' ', last_name) = @fn;";
            MySqlCommand a = new MySqlCommand(query);
            a.Parameters.AddWithValue("@fn", doctor);
            dbConnector.setValues(a, "07");
            databaseResponse = dbConnector.executeQuery();

            string query2 = "INSERT INTO appointmentbookingdetail (physician_id,patient_id,date,time,cause) values (@physician_id,@patient_id,@date,@time,'regular check up');";
            MySqlCommand b = new MySqlCommand(query2);
            b.Parameters.AddWithValue("@physician_id", databaseResponse[0]);
            b.Parameters.AddWithValue("@patient_id", patient);
            b.Parameters.AddWithValue("@date", apptDate);
            b.Parameters.AddWithValue("@time", apptTime);
            dbConnector.setValues(b, "03");
            databaseResponse.Clear();
            databaseResponse = dbConnector.executeQuery();

            //bookHelper(databaseResponse[0]);
            sendBack.Add("07");
            sendBack.Add(databaseResponse[0]);
            return sendBack;
        }
        private void bookHelper(string p_id)
        {
            string query = "INSERT INTO appointmentbookingdetail (physician_id,patient_id,date,time,cause) values (@physician_id,@patient_id,@date,@time,'regular check up');";
            databaseResponse.Clear();
            MySqlCommand a = new MySqlCommand(query);
            a.Parameters.AddWithValue("@physician_id", p_id);
            a.Parameters.AddWithValue("@patient_id", patient);
            a.Parameters.AddWithValue("@date", apptDate);
            a.Parameters.AddWithValue("@time", apptTime);
            dbConnector.setValues(a, "03");
            databaseResponse = dbConnector.executeQuery();
        }
    }
}
