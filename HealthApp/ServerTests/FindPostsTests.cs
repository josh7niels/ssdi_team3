﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Tests
{
    [TestClass()]
    public class FindPostsTests
    {
        [TestMethod()]
        public void executeTest()
        {
            List<string> actual = new List<string>();
            FindPosts find = new FindPosts();
            IDBConnect dbConnector = new dbCommTEST();
            find.setDBConnectInstance(dbConnector);
            actual = find.execute();
            List<string> expected = new List<string>();
            expected.Add("08");
            expected.Add("content, 12/01/2015, 09:00:00");
            expected.Add("content2, 12/02/2015, 10:10:10");
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }
    }
}