using System;
using MySql.Data.MySqlClient;

namespace Server
{
    class database
    {
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        MySqlDataReader reader = null;
        string str = "Server=localhost;Database=health;Uid=root;Pwd=mustang;";

        public bool validate_Login(string id, string pass)
        {
            string str2 = "Select * from login where u_id=@u_id and password=@password;";
            {

                con = new MySqlConnection(str);

                con.Open();  //open the connection

                cmd = new MySqlCommand(str2, con);

                //we will bound a value to the placeholder

                try
                {
                    con = new MySqlConnection(str);
                    con.Open();//open the connection
                    cmd.Prepare();
                    cmd = new MySqlCommand(str2, con);
                    //we will bound a value to the placeholder
                    cmd.Parameters.AddWithValue("@u_id", id);
                    cmd.Parameters.AddWithValue("@password", pass);
                    cmd.ExecuteNonQuery();
                    reader = cmd.ExecuteReader();
                }
                catch (Exception err)
                {
                    Console.WriteLine("Error: " + err.ToString());
                }
            }

            if (reader.Read())
            {
                con.Close();
                reader.Close();
                return true;
            }
            else
            {
                con.Close();
                reader.Close();
                return false;
            }
        }
    }
    
}