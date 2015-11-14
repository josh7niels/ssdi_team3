using Microsoft.VisualStudio.TestTools.UnitTesting;
using HealthApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp.Tests
{
    [TestClass()]
    public class CoreTests
    {
        IList<string> list = new List<string>();
        [TestMethod()]
        public void CoreTest()
        {
            list.Add("192.168.1.1");
            list.Add("jnielse5@uncc.edu");
            Core myCore = new Core(list);
            string expectedIP = "192.168.1.1";
            string expectedUN = "jnielse5@uncc.edu";
            Assert.AreEqual(expectedIP, myCore.ipAddress);
            Assert.AreEqual(expectedUN, myCore.username);
        }

        [TestMethod()]
        public void messageHandlerTest()
        {
        }
    }
}