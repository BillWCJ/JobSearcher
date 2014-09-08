using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Collections.Specialized;
using GlobalVariable;
using ConsoleApplication;
using ContentProcess;
using WebClientExtension;
using Jobs;


namespace ConsoleApplication.Test {
    [TestClass]
    public class ConsoleApplicationTest {

        [TestMethod]
        public void PrinterString()
        {
            Trace.WriteLine("id=\"UW_CO_JOBDTL_DW_UW_CO_EMPUNITDIV\">");
            Assert.IsTrue(true);
        }
    }
}
