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
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        string str = ConfigurationManager.AppSettings.Get("dbConnectionString");
        public BookAppointment(string doc, string pat, string date, string time)
        {
            string[] a;
            a = date.Split('/');
            patient = pat;
            doctor = doc;
            apptDate = a[2] + "-" + a[0] + "-" + a[1];
            apptTime = time;
        }
        public List<string> execute()
        {
            sendBack.Add("07");
            string p_id = null;
            try
            {
                string query = "Select u_id from user where concat(first_name, ' ', last_name) = @fn;";
                con = new MySqlConnection(str);
                con.Open();
                cmd = new MySqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@fn", doctor);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                Console.WriteLine("reader executing...");
                while (reader.Read())
                {
                    Console.WriteLine("reader reading...");
                    p_id = reader.GetString(0);
                }
            }
            catch(MySqlException e)
            {
                throw e;
            }
                reader.Close();
                con.Close();
                bookHelper(p_id);
            return sendBack;
        }
        private void bookHelper(string p_id)
        {
            Console.WriteLine("Book Helper");
            con = new MySqlConnection(str);
            con.Open();
            try
            {
                string query = "INSERT INTO appointmentbookingdetail (physician_id,patient_id,date,time,cause) values (@physician_id,@patient_id,@date,@time,'regular check up');";
                cmd = new MySqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@physician_id", p_id);
                cmd.Parameters.AddWithValue("@patient_id", patient);
                cmd.Parameters.AddWithValue("@date", apptDate);
                cmd.Parameters.AddWithValue("@time", apptTime);
                Console.WriteLine("Book Helper - execute query");
                if (cmd.ExecuteNonQuery() == 1)
                    sendBack.Add("1");
                else
                    sendBack.Add("0");

            }
            catch (MySqlException e)
            {
                throw e;
            }
            
        }
    }
}
