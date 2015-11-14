using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    class FindDoctors : IMessage
    {
        List<string> docList = new List<string>();
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        string str = "Server=localhost;Database=health;Uid=root;Pwd=mustang;";
        public FindDoctors()
        {
            getDoctors();
        }
        private void getDoctors()
        {
            docList.Add("04");
            try
            {
                string query = "SELECT concat(first_name,' ',last_name) from user where role='D';";
                con = new MySqlConnection(str);
                con.Open();
                cmd = new MySqlCommand(query, con);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        docList.Add(reader.GetString(0));

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
            return docList;
        }
    }
}
