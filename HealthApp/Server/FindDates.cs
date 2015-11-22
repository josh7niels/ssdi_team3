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
        List<string> databaseResponse = new List<string>();
        IDBConnect dbConnector = null;
        public FindDates(string doc)
        {
            doctor = doc;
        }
        public void setDBConnectInstance(IDBConnect db)
        {
            dbConnector = db;
        }
        public List<string> execute()
        {
            string query = "Select Date(date), concat(first_name,' ', last_name) as name "
                            + " from appointmentbookingdetail a join user u on u.u_id = a.physician_id "
                            + " where concat(first_name, ' ', last_name) = @fn and date >= current_date "
                            + " group by physician_id,date having count(date) = 8 ;";
            MySqlCommand a = new MySqlCommand(query);
            a.Parameters.AddWithValue("@fn", doctor);
            dbConnector.setValues(a, "05");
            databaseResponse = dbConnector.executeQuery();
            string x,y;
            for (int i = 0; i < databaseResponse.Count; i++)
            {
                x = databaseResponse[i];
                y = x.Split(' ').First();
                dateList.Add(y);
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
            
            sendBack.Insert(0, "05");
            return sendBack;
        }
    }
}
