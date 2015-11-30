using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    class FindReplies : IMessage
    {
        List<string> sendBack = new List<string>();
        List<string> databaseResponse = new List<string>();
        IDBConnect dbConnector = null;
        string postID;
        public FindReplies(string id)
        {
            postID = id;
        }
        public void setDBConnectInstance(IDBConnect db)
        {
            dbConnector = db;
        }
        public List<string> execute()
        {
            string query = "select 'anonymous',content,post_date,post_time from reply_Forum join user on u_id=mem_id "
                    + "WHERE role = 'U'and p_forum_id = @p_forum_id union "
                    + "SELECT concat(first_name, ' ', last_name),content,post_date,post_time from reply_Forum join user on u_id = mem_id "
                    + "WHERE role = 'D' and p_forum_id = @p_forum_id order by post_time;";
            MySqlCommand a = new MySqlCommand(query);
            a.Parameters.AddWithValue("@p_forum_id", postID);
            dbConnector.setValues(a, "09");
            databaseResponse = dbConnector.executeQuery();
            sendBack.Add("09");
            for (int i = 0; i < databaseResponse.Count; i++)
            {
                sendBack.Add(databaseResponse[i]);
            }
            return sendBack;
        }
    }
}
