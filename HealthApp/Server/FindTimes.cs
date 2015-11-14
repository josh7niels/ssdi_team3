using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    class FindTimes : IMessage
    {
        string apptDate, doctor;
        List<string> timeListF = new List<string> { "09:00:00", "10:00:00", "11:00:00", "12:00:00", "01:00:00", "02:00:00", "03:00:00", "04:00:00", "05:00:00"};
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        string str = "Server=localhost;Database=health;Uid=root;Pwd=mustang;";
        public FindTimes(string date, string doc)
        {
            apptDate = date;
            doctor = doc;
            getTimes();
        }
        private void getTimes()
        {
            List<string> timeList = new List<string>();
            try
            {
                string query = "select time from appointmentbookingdetail a join user u on u.u_id=a.physician_id where concat(first_name, ' ', last_name) = @fn and  date = @date;";
                con = new MySqlConnection(str);
                con.Open();
                cmd = new MySqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@date", apptDate);//binding parameter selected date
                cmd.Parameters.AddWithValue("@fn", doctor);//binding parameter selected name
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        timeList.Add(reader.GetString(0));
                    }
                }                
                reader.Close();
                con.Close();
            }
            catch (MySqlException err)
            {
                throw err;
            }
            Console.WriteLine(".............from list of time slots booked.......");
            foreach (string s in timeList)
            {
                Console.WriteLine(s);
                timeListF.Remove(s);
            }
        }
        public List<string> getResponse()
        {
            timeListF.Insert(0, "06");
            return timeListF;
        }
    }
}
