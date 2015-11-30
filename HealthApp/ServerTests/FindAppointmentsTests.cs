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
    public class FindAppointmentsTests
    {
        [TestMethod()]
        public void executeTest()
        {
            //prepare test
            List<string> actual = new List<string>();
            FindAppointments myFA = new FindAppointments("jnielse5@uncc.edu");
            IDBConnect dbConnector = new dbCommTEST();
            myFA.setDBConnectInstance(dbConnector);
            actual = myFA.execute();
            List<string> expected = new List<string>();
            expected.Add("02");
            expected.Add("1, Doctor, Patient, 12/12/2012, 09:00:00");
            expected.Add("2, Doctor2, Patient2, 01/01/2001, 11:00:00");
            expected.Add("3, Doctor3, Patient3, 11/25/2015, 10:00:00");
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }
    }
}