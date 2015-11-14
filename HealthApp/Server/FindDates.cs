using System;
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
        List<string> availableDateList = new List<string>();
        List<string> dateList = new List<string>();
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        string str = "Server=localhost;Database=health;Uid=root;Pwd=mustang;";
        public FindDates(string doc)
        {
            doctor = doc;
            //getDates();
        }
        private void getDates()
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
                    Console.WriteLine("has rows");
                    while (reader.Read())
                    {
                        dateList.Add(reader.GetString(0));

                    }
                }
                DateTime dat = DateTime.Today;
                for (int i = 0; i <= 30; i++)
                {
                    DateTime d1 = dat.AddDays(i);
                    string d = d1.ToString();
                    Console.WriteLine(d);
                    //if(d1.)
                    availableDateList.Add(d);
                }
                foreach (string date in dateList)
                {
                    availableDateList.Remove(date);
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
            availableDateList.Insert(0, "05");
            return availableDateList;
        }
    }
}
