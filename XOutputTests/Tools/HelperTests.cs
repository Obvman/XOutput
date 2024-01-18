﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XOutput.Tools.Tests
{
    [TestClass()]
    public class HelperTests
    {
        [DataRow(5, 5, true)]
        [DataRow(5, 5.0000001, true)]
        [DataRow(5, 5.1, false)]
        [DataRow(4.9, 5, false)]
        [DataTestMethod]
        public void DoubleEquals(double a, double b, bool equals)
        {
            Assert.AreEqual(equals, Helper.DoubleEquals(a, b));
        }
    }
}