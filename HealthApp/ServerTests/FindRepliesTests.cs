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
    public class FindRepliesTests
    {
        [TestMethod()]
        public void FindRepliesTest()
        {
            List<string> actual = new List<string>();
            FindReplies findR = new FindReplies("1");
            IDBConnect dbConnector = new dbCommTEST();
            findR.setDBConnectInstance(dbConnector);
            actual = findR.execute();
            List<string> expected = new List<string>();
            expected.Add("09");
            expected.Add("anonymous,content, 12/01/2015, 09:00:00");
            expected.Add("DoctorA,content2, 12/02/2015, 10:10:10");
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }
    }
}