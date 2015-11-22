using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server
{
    public class dbCommTEST : IDBConnect
    {
        List<string> sendBack = new List<string>();
        string type;
        public void setValues(MySqlCommand c, string t)
        {
            type = t;
        }
        public List<string> executeQuery()
        {
            switch (type)
            {
                case "01":
                    sendBack.Add("1");
                    sendBack.Add("Joshua Nielsen");
                    break;
            }
            return sendBack;
        }
    }
}
