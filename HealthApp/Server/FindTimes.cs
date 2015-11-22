using System;
using System.Configuration;
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
        List<string> sendBack = new List<string> { "09:00:00", "10:00:00", "11:00:00", "12:00:00", "01:00:00", "02:00:00", "03:00:00", "04:00:00", "05:00:00"};
        List<string> databaseResponse = new List<string>();
        public FindTimes(string date, string doc)
        {
            apptDate = date;
            doctor = doc;
        }
        public List<string> execute()
        {
            string query = "select time from appointmentbookingdetail a join user u on u.u_id=a.physician_id where concat(first_name, ' ', last_name) = @fn and  date = @date;";
            MySqlCommand a = new MySqlCommand(query);
            a.Parameters.AddWithValue("@date", apptDate);
            a.Parameters.AddWithValue("@fn", doctor);
            databaseCommunicator myDB = new databaseCommunicator(a, "06");
            databaseResponse = myDB.executeQuery();

            foreach (string s in databaseResponse)
            {
                sendBack.Remove(s);
            }
            sendBack.Insert(0, "06");
            return sendBack;
        }
    }
}
