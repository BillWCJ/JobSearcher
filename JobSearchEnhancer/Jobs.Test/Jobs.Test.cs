using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Entities;
using System.Linq;
using System.Diagnostics;
using GlobalVariable;

namespace Jobs.Test {
    [TestClass]
    public class JobTest {
        [TestMethod]
        public void FULLID_ShouldReturnIDWithCorrectLength_WhenExecute() {
            Employer employer = new Employer{Id=00241045,Name="Toronto Transit Commission"};
            string fullIDString = employer.IdString;
            Trace.WriteLine(fullIDString);
            Assert.IsTrue(fullIDString == "00241045", "FULLID Does not return correct Length ID");
        }

        //[TestMethod]
        //public void Disciplines_ShouldContainALLDisciplines_WhenPassedAllDisciplines()
        //{
        //    bool[] baseDisciplineArgument = Enumerable.Repeat(true, Discipline.DisciplinesNames.Length).ToArray();
        //    Discipline baseDisciplines = new Discipline(baseDisciplineArgument);
        //    string testData = string.Empty;
        //    foreach (string discipline in Discipline.DisciplinesNames)
        //        testData += discipline + ",";
        //    Discipline testDisciplines = new Discipline(testData);
        //    Assert.IsTrue(testDisciplines.ToString() == baseDisciplines.ToString(), "ExtractDisciplines did not extract correctly");
        //}

    }
}

