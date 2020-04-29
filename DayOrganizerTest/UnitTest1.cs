using System;
using DatabaseAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DayOrganizerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DatabaseCore.TestRun();
        }
    }
}
