using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobMine;
using GlobalVariable;

namespace JobMine.Test
{
    [TestClass]
    public class JobInquiryTest
    {
        [TestMethod]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedCorrectID1()
        {
            Assert.IsTrue(JobInquiry.IsCorrectJobID(GVar.TestJobID));
        }

        [TestMethod]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedCorrectID2()
        {
            Assert.IsTrue(JobInquiry.IsCorrectJobID("12345678"));
        }

        [TestMethod]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedInccorrectformatID()
        {
            Assert.IsFalse(JobInquiry.IsCorrectJobID("1234567e"));
        }

        [TestMethod, Ignore]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedInccorrectLongID1()
        {
            Assert.IsFalse(JobInquiry.IsCorrectJobID("0" + GVar.TestJobID));
        }

        [TestMethod, Ignore]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedInccorrectLongID2()
        {
            Assert.IsFalse(JobInquiry.IsCorrectJobID(GVar.TestJobID + "0"));
        }

        [TestMethod, Ignore]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedInccorrectLongID3()
        {
            Assert.IsFalse(JobInquiry.IsCorrectJobID("123456789"));
        }

        [TestMethod]
        public void IsCorrectJobID_ShouldReturnTrue_WhenPassedIccorrectShortID()
        {
            Assert.IsFalse(JobInquiry.IsCorrectJobID(GVar.TestJobID.Substring(GVar.TestJobID.Length - 1)));
        }
    }
}
