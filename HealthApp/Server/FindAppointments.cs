using System;
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
        List<string> apptList = new List<string>();
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        string str = "Server=localhost;Database=health;Uid=root;Pwd=mustang;";
        public FindAppointments(string un)
        {
            username = un;
            displayAppointments();
            
        }
        private void displayAppointments()
        {
            apptList.Add("02");
            try
            {
                string query = "select appointment_id,physician_id,date,time,cause from appointmentbookingdetail where patient_id=@patient_id order by appointment_id desc;";
                con = new MySqlConnection(str);
                con.Open();
                cmd = new MySqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@patient_id", username);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string temp = reader.GetString(2).Split(' ').First();
                        string appts = reader.GetString(0) + "," +  reader.GetString(1) + "," + temp + "," + reader.GetString(3) + "," + reader.GetString(4);
                        apptList.Add(appts);
                    }
                }
                reader.Close();
                con.Close();
            }

            catch (MySqlException err)
            {
                throw err;
            }
        }

        public List<string> getResponse()
        {
            return apptList;
        }
    }
}
