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
            BookAppointment my = new BookAppointment("Magdellena Billiska", "mnaga@uncc.edu", "2015-12-04", "09:00:00");

            Assert.Fail();
        }

        [TestMethod()]
        public void executeTest()
        {
            Assert.Fail();
        }
    }
}