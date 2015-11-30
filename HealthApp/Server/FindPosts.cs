using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    public class FindPosts : IMessage
    {
        List<string> sendBack = new List<string>();
        List<string> databaseResponse = new List<string>();
        IDBConnect dbConnector = null;
        public void setDBConnectInstance(IDBConnect db)
        {
            dbConnector = db;
        }
        public List<string> execute()
        {
            string query = "select forum_id,content,post_date,post_time from question_Forum;";
            MySqlCommand a = new MySqlCommand(query);
            dbConnector.setValues(a, "08");
            databaseResponse = dbConnector.executeQuery();
            sendBack.Add("08");
            for (int i = 0; i < databaseResponse.Count; i++)
            {
                sendBack.Add(databaseResponse[i]);
            }
            return sendBack;
        }
    }
}
