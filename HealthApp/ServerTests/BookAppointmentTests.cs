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
            List<string> actual = new List<string>();
            BookAppointment book = new BookAppointment("DoctorA", "patient", "12/10/2015", "09:00:00");
            IDBConnect dbConnector = new dbCommTEST();
            book.setDBConnectInstance(dbConnector);
            actual = book.execute();
            List<string> expected = new List<string>();
            expected.Add("07");
            expected.Add("1");
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }
    }
}