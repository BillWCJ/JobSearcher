using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jobs;
using System.Linq;
using System.Diagnostics;
using GlobalVariable;

namespace Jobs.Test {
    [TestClass]
    public class JobTest {
        [TestMethod]
        public void FULLID_ShouldReturnIDWithCorrectLength_WhenExecute() {
            Employer employer = new Employer(00241045, "Toronto Transit Commission");
            string fullIDString = employer.EmployerIDString;
            Trace.WriteLine(fullIDString);
            Assert.IsTrue(fullIDString == "00241045", "FULLID Does not return correct Length ID");
        }

        [TestMethod]
        public void Disciplines_ShouldContainALLDisciplines_WhenPassedAllDisciplines()
        {
            bool[] baseDisciplineArgument = Enumerable.Repeat(true, GVar.DisciplinesNames.Length).ToArray();
            Disciplines baseDisciplines = new Disciplines(baseDisciplineArgument);
            string testData = string.Empty;
            foreach (string discipline in GVar.DisciplinesNames)
                testData += discipline + ",";
            Disciplines testDisciplines = new Disciplines(testData);
            Assert.IsTrue(testDisciplines.ToString() == baseDisciplines.ToString(), "ExtractDisciplines did not extract correctly");
        }

    }
}

