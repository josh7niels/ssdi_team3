using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    class FindDates : IMessage
    {
        string doctor;
        List<string> sendBack = new List<string>();
        List<string> dateList = new List<string>();
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        string str = ConfigurationManager.AppSettings.Get("dbConnectionString");
        public FindDates(string doc)
        {
            doctor = doc;
        }
        public List<string> execute()
        {
            dateList.Add("05");
            try
            {
                string query = "Select Date(date), concat(first_name,' ', last_name) as name "
                            + " from appointmentbookingdetail a join user u on u.u_id = a.physician_id "
                            + " where concat(first_name, ' ', last_name) = @fn and date >= current_date "
                            + " group by physician_id,date having count(date) = 8 ;";
                con = new MySqlConnection(str);
                con.Open();
                cmd = new MySqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@fn", doctor);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    string a,b;
                    while (reader.Read())
                    {
                        a = reader.GetString(0);
                        b = a.Split(' ').First();
                        dateList.Add(b);

                    }
                }
                DateTime dat = DateTime.Today;
                for (int i = 0; i <= 30; i++)
                {
                    DateTime d1 = dat.AddDays(i);
                    string d = d1.ToString();
                    string e = d.Split(' ').First();
                    sendBack.Add(e);
                }
                foreach (string date in dateList)
                {
                    sendBack.Remove(date);
                }
                reader.Close();
                con.Close();
                sendBack.Insert(0, "05");
                return sendBack;
            }
            catch (MySqlException err)
            {
                throw err;
            }

        }
    }
}
