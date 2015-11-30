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
    public class FindTimesTests
    {
        [TestMethod()]
        public void DatesReturned()
        {
            List<string> actual = new List<string>();
            FindTimes find = new FindTimes("12/05/2015","doctorA");
            IDBConnect dbConnector = new dbCommTEST();
            find.setDBConnectInstance(dbConnector);
            actual = find.execute();
            List<string> expected = new List<string>();
            expected.Add("06");
            expected.Add("11:00:00");
            expected.Add("12:00:00");
           
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }
        public void checkBusyDatesNotDisplayed()
        {
            List<string> actual = new List<string>();
            FindTimes find = new FindTimes("12/05/2015", "doctorA");
            IDBConnect dbConnector = new dbCommTEST();
            find.setDBConnectInstance(dbConnector);
            actual = find.execute();
            List<string> expected = new List<string>();
            expected.Add("06");
            expected.Add("10:00:00");
            //expected.Add("12:00:00");

            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreNotEqual(expected[1], actual[1]);
            //Assert.AreEqual(expected[2], actual[2]);
        }

    }
}