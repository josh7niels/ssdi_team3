using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    class NewPost : IMessage
    {
        List<string> sendBack = new List<string>();
        List<string> databaseResponse = new List<string>();
        IDBConnect dbConnector = null;
        string postID, content, postDate, postTime;
        public NewPost(string id, string c, string date, string time)
        {
            postID = id;
            content = c;
            postDate = date;
            postTime = time;
        }
        public void setDBConnectInstance(IDBConnect db)
        {
            dbConnector = db;
        }
        public List<string> execute()
        {
            string query = "Insert into question_Forum(mem_id,content,post_date,post_time) "
                        + "VALUES(@mem_id, @content, @post_date, @post_time);";
            MySqlCommand a = new MySqlCommand(query);
            a.Parameters.AddWithValue("@mem_id", postID);
            a.Parameters.AddWithValue("@content", content);
            a.Parameters.AddWithValue("@postDate", postDate);
            a.Parameters.AddWithValue("@postTime", postTime);
            dbConnector.setValues(a, "03");
            databaseResponse = dbConnector.executeQuery();
            sendBack.Add("10");
            sendBack.Add(databaseResponse[0]);

            return sendBack;
        }
    }
}
