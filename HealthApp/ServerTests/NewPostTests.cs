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
    public class NewPostTests
    {
        [TestMethod()]
        public void NewPostTest()
        {
            List<string> actual = new List<string>();
            NewPost newP = new NewPost("id","content","12/10/2015","09:00:00");
            IDBConnect dbConnector = new dbCommTEST();
            newP.setDBConnectInstance(dbConnector);
            actual = newP.execute();
            List<string> expected = new List<string>();
            expected.Add("10");
            expected.Add("1");
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            //Assert.AreEqual(expected[2], actual[2]);
        }
        public void nullObjectParametercheck()
        {
            
            try
            {
                var obj = new NewPost(null,null,null,null);
                Assert.Fail("An exception should have been thrown");
            }
            catch (ArgumentNullException ae)
            {
                Assert.AreEqual("Parameter cannot be null or empty.", ae.Message);
            }
            catch (Exception e)
            {
                Assert.Fail(
                     string.Format("Unexpected exception of type {0} caught: {1}",
                                    e.GetType(), e.Message)
                );
            }
        }
    }
}