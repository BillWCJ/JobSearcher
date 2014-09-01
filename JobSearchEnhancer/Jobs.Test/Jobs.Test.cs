using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jobs;
using System.Diagnostics;

namespace Jobs.Test {
    [TestClass]
    public class JobTest {


        [TestMethod]
        public void FULLID_ShouldReturnIDWithCorrectLength_WhenExecute() {
            Employer employer = new Employer("Toronto Transit Commission", 00241045);
            string fullIDString = employer.FullIDString();
            Trace.WriteLine(fullIDString);
            Assert.IsTrue(fullIDString == "00241045", "FULLID Does not return correct Length ID");
        }

    }
}

