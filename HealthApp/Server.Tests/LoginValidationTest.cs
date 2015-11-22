// <copyright file="LoginValidationTest.cs">Copyright ©  2015</copyright>
using System;
using System.Collections.Generic;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;

namespace Server.Tests
{
    /// <summary>This class contains parameterized unit tests for LoginValidation</summary>
    [PexClass(typeof(LoginValidation))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class LoginValidationTest
    {
        /// <summary>Test stub for .ctor(String, String)</summary>
        [PexMethod]
        internal LoginValidation ConstructorTest(string un, string pass)
        {
            LoginValidation target = new LoginValidation(un, pass);
            return target;

            // TODO: add assertions to method LoginValidationTest.ConstructorTest(String, String)
        }

        /// <summary>Test stub for execute()</summary>
        [PexMethod]
        internal List<string> executeTest([PexAssumeUnderTest]LoginValidation target)
        {
            List<string> result = target.execute();
            return result;
            List<string> expected = new List<string>();
            expected.Add("01");
            expected.Add("1");
            expected.Add("Monisha Naga");
            Assert.AreEqual(expected[0], result[0]);
            Assert.AreEqual(expected[1], result[1]);
            Assert.AreEqual(expected[2], result[2]);
            // TODO: add assertions to method LoginValidationTest.executeTest(LoginValidation)
        }
    }
}
