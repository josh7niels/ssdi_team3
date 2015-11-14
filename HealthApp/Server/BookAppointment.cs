using System;
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
        MySqlConnection con = null/*, con1 = null*/;
        MySqlCommand cmd = null/*, cmd2 = null*/;
        MySqlDataReader reader = null;
        string str = "Server=localhost;Database=health;Uid=root;Pwd=mustang;";
        public BookAppointment(string doc, string pat, string date, string time)
        {
            patient = pat;
            doctor = doc;
            apptDate = date;
            apptTime = time;
            book();
        }
        private void book()
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
                /*Console.WriteLine("doctor id: " + p_id);
                query = "INSERT INTO appointmentbookingdetail"
                            + "(physician_id,patient_id,date,time,cause)"
                            + " values (@physician_id,@patient_id,@date,@time,'regular check up');";
                con1 = new MySqlConnection(str);
                con1.Open();
                cmd2 = new MySqlCommand(query, con1);
                cmd2.Prepare();
                cmd2.Parameters.AddWithValue("@physician_id", p_id);
                cmd2.Parameters.AddWithValue("@patient_id", patient);
                cmd2.Parameters.AddWithValue("@date", apptDate);
                cmd2.Parameters.AddWithValue("@time", apptTime);
                Console.WriteLine(p_id + ", " + patient + ", " + apptDate + ", " + apptTime);
                //int numberOfRecords = cmd2.ExecuteNonQuery();
                Console.WriteLine("second query executed");
                if (cmd.ExecuteNonQuery() == 1)
                    sendBack.Add("1");
                else
                    sendBack.Add("0");
                Console.WriteLine("Complete... ready for sendback");
            }
            catch (MySqlException err)
            {
                throw err;
            }
            con1.Close();*/
        }
        private void bookHelper(string p_id)
        {
            con = new MySqlConnection(str);
            con.Open();
            try
            {
                string query = "INSERT INTO appointmentbookingdetail"
                            + "(physician_id,patient_id,date,time,cause)"
                            + " values (@physician_id,@patient_id,@date,@time,'regular check up');";
                cmd = new MySqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@physician_id", p_id);
                cmd.Parameters.AddWithValue("@patient_id", patient);
                cmd.Parameters.AddWithValue("@date", apptDate);
                cmd.Parameters.AddWithValue("@time", apptTime);
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
        public List<string> getResponse()
        {
            return sendBack;
        }
    }
}
