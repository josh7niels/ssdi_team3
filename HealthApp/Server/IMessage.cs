﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface IMessage
    {
        List<string> execute();
        void setDBConnectInstance(IDBConnect db);
    }
}
