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
    public class FindDoctorsTests
    {
        [TestMethod()]
        public void FindDoctorsTest()
        {
            List<string> actual = new List<string>();
            FindDoctors find = new FindDoctors();
            IDBConnect dbConnector = new dbCommTEST();
            find.setDBConnectInstance(dbConnector);
            actual = find.execute();
            List<string> expected = new List<string>();
            expected.Add("04");
            expected.Add("doctorA");
            expected.Add("doctorB");
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }
    }
}