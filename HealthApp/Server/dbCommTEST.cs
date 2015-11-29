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
                case "02":
                    sendBack.Add("1, Doctor, Patient, 12/12/2012, 09:00:00");
                    sendBack.Add("2, Doctor2, Patient2, 01/01/2001, 11:00:00");
                    sendBack.Add("3, Doctor3, Patient3, 11/25/2015, 10:00:00");
                    break;
                case "03":
                    sendBack.Add("1");
                    break;
            }
            return sendBack;
        }
    }
}
