using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    public class NewReply : IMessage
    {
        List<string> sendBack = new List<string>();
        List<string> databaseResponse = new List<string>();
        IDBConnect dbConnector = null;
        string postID, content, postDate, postTime, username;
        public NewReply(string id, string u, string c, string date, string time)
        {
            postID = id;
            content = c;
            postDate = date;
            postTime = time;
            username = u;
        }
        public void setDBConnectInstance(IDBConnect db)
        {
            dbConnector = db;
        }
        public List<string> execute()
        {
            Console.WriteLine("NewReply executing...");
            string query = "Insert into reply_Forum(p_forum_id,mem_id,content,post_date,post_time) "
                        + "VALUES(@p_forum_id, @mem_id, @content, @post_date, @post_time);";
            MySqlCommand a = new MySqlCommand(query);
            a.Parameters.AddWithValue("@p_forum_id", postID);
            a.Parameters.AddWithValue("@mem_id", username);
            a.Parameters.AddWithValue("@content", content);
            a.Parameters.AddWithValue("@post_date", postDate);
            a.Parameters.AddWithValue("@post_time", postTime);
            dbConnector.setValues(a, "03");
            databaseResponse = dbConnector.executeQuery();
            sendBack.Add("11");
            sendBack.Add(databaseResponse[0]);

            return sendBack;
        }
    }
}
