using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    class dbConnect
    {
        public MySqlConnection getConnection()
        {
            string con = ConfigurationManager.AppSettings.Get("dbConnectionString");
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            return connection;
        }
        /*public string connectionFile()
        {
            int counter = 0;
            string line;
            string str = null;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader("C:\\Users\\monisha\\test.txt");// file name and location with connection string
            while ((line = file.ReadLine()) != null)
            {
                str = line;
                Console.WriteLine(line);
                counter++;
            }

            file.Close();
            return str;
        }*/
    }
}


namespace HealthPhase2
{
    class ConnectionFileClass
    {
        public string connectionFile()
        {
            int counter = 0;
            string line;
            string str = null;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader("C:\\Users\\monisha\\test.txt");// file name and location with connection string
            while ((line = file.ReadLine()) != null)
            {
                str = line;
                Console.WriteLine(line);
                counter++;
            }

            file.Close();
            return str;
        }
    }
}
