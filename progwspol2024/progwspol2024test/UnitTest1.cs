using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace progwspol2024test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsTrue(true);
        }
    }

    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsFalse(false);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreNotEqual(1, 2);
        }
    }
}
