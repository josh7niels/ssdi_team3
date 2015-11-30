using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Tests
{
    [TestClass()]
    public class DeleteAppointmentTests
    {
        [TestMethod()]
        public void executeTest()
        {
            //prepare test
            List<string> actual = new List<string>();
            DeleteAppointment myDA = new DeleteAppointment("25");
            IDBConnect dbConnector = new dbCommTEST();
            myDA.setDBConnectInstance(dbConnector);
            actual = myDA.execute();
            //prepare expected values
            List<string> expected = new List<string>();
            expected.Add("03");
            expected.Add("1");
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }
    }
}