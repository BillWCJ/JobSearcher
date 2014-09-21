using System;
using System.IO;
using System.Web;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlobalVariable;
using Jobs;
using ContentProcess;
using WebClientExtension;

namespace ContentProcess.Test
{
    [TestClass]
    public class ContentProcessTest
    {
        [TestMethod]
        public void ConfirmLogin_ShouldReturnTrue_WhenExecute()
        {
            Assert.IsTrue(ContentExtraction.LoginToJobmine(new CookieEnabledWebClient()));
        }

        [TestMethod]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedCorrectID1()
        {
            Assert.IsTrue(ContentExtraction.IsCorrectJobID(GVar.TestJobID));
        }

        [TestMethod]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedCorrectID2()
        {
            Assert.IsTrue(ContentExtraction.IsCorrectJobID("12345678"));
        }

        [TestMethod]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedInccorrectformatID()
        {
            Assert.IsFalse(ContentExtraction.IsCorrectJobID("1234567e"));
        }

        [TestMethod , Ignore]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedInccorrectLongID1()
        {
            Assert.IsFalse(ContentExtraction.IsCorrectJobID("0" + GVar.TestJobID));
        }

        [TestMethod, Ignore]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedInccorrectLongID2()
        {
            Assert.IsFalse(ContentExtraction.IsCorrectJobID(GVar.TestJobID + "0"));
        }

        [TestMethod, Ignore]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedInccorrectLongID3()
        {
            Assert.IsFalse(ContentExtraction.IsCorrectJobID("123456789"));
        }

        [TestMethod]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedIccorrectShortID()
        {
            Assert.IsFalse(ContentExtraction.IsCorrectJobID(GVar.TestJobID.Substring(GVar.TestJobID.Length - 1)));
        }



    }
}
