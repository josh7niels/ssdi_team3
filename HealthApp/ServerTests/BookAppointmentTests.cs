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
    public class BookAppointmentTests
    {
        [TestMethod()]
        public void BookAppointmentTest()
        {
            BookAppointment appt = new BookAppointment("a","b","c","d");
            Assert.Fail();
        }

        [TestMethod()]
        public void getResponseTest()
        {
            Assert.Fail();
        }
    }
}