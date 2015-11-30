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
    public class FindDatesTests
    {
        [TestMethod()]
        public void FindDatesTest()
        {
            List<string> actual = new List<string>();
            FindDates find = new FindDates("DoctorA");
            IDBConnect dbConnector = new dbCommTEST();
            find.setDBConnectInstance(dbConnector);
            actual = find.execute();
            List<string> expected = new List<string>();
            expected.Add("05");
            expected.Add("11/30/2015");
            expected.Add("12/2/2015");
            expected.Add("12/4/2015");
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }
    }
}