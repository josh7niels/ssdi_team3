using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    class DeleteAppointment : IMessage
    {
        string apptID;
        List<string> sendBack = new List<string>();
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        string str = ConfigurationManager.AppSettings.Get("dbConnectionString");
        public DeleteAppointment(string id)
        {
            apptID = id;
        }
        public List<string> execute()
        {
            Console.WriteLine("delete method");
            sendBack.Add("03");
            try
            {
                string query = "delete from appointmentbookingdetail where appointment_id = @appointment_id ;";
                con = new MySqlConnection(str);
                Console.WriteLine("connection instantiated");
                con.Open();
                Console.WriteLine("connection opened");
                cmd = new MySqlCommand(query, con);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@appointment_id", apptID);
                Console.WriteLine("Recieved Appointment ID: " + apptID);
                Console.WriteLine("executing query...");
                if (cmd.ExecuteNonQuery() == 1)
                    sendBack.Add("1");
                else
                    sendBack.Add("0");
                con.Close();
            }
            catch (MySqlException err)
            {
                throw err;
            }
            return sendBack;
        }
    }
}
