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
    public class LoginValidationTests
    {

        [TestMethod()]
        public void executeTest()
        {
            //prepare test
            List<string> actual = new List<string>();
            LoginValidation myLV = new LoginValidation("myusername", "mypassword");
            IDBConnect dbConnector = new dbCommTEST();
            myLV.setDBConnectInstance(dbConnector);
            actual = myLV.execute();
            //prepare expected values
            List<string> expected = new List<string>();
            expected.Add("01");
            expected.Add("1");
            expected.Add("Joshua Nielsen");
            Assert.AreEqual(expected[0],actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }
    }
}