using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public interface IDBConnect
    {
        List<string> executeQuery();
        void setValues(MySqlCommand c, string t);
    }
}
