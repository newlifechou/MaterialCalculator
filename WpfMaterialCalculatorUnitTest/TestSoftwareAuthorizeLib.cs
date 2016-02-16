using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareAuthorizeLib;

namespace WpfMaterialCalculatorUnitTest
{
    [TestClass]
    public class TestSoftwareAuthorizeLib
    {
        private Permisson tester;
        public TestSoftwareAuthorizeLib()
        {
            tester = new Permisson();
        }

        [TestMethod]
        public void TestCheckTimeOk()
        {
            Assert.IsTrue(tester.CheckTime(new DateTime(2016,2,17)));
        }

        [TestMethod]
        public void TestCheckTimeNotOK()
        {
            Assert.IsFalse(tester.CheckTime(new DateTime(2016, 2, 16)));
        }








    }
}
