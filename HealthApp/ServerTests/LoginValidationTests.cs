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
        public void LoginValidationTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void executeTest()
        {
            LoginValidation target = new LoginValidation("mnaga@uncc.edu", "Nihu*sept21");
            List<string> result = target.execute();
            List<string> expected = new List<string>();
            expected.Add("01");
            expected.Add("1");
            expected.Add("Monisha Naga");
            Assert.AreEqual(expected[0], result[0]);
            Assert.AreEqual(expected[1], result[1]);
            Assert.AreEqual(expected[2], result[2]);
        }
    }
}