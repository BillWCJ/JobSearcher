using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentProcess;
using GlobalVariable;
using Jobs;
using WebClientExtension;

namespace ContentProcess.Test
{
    [TestClass]
    public class ContentProcessTest
    {
        [TestMethod]
        public void ExtractDisciplines_ShouldContainALLDisciplines_WhenPassedAllDisciplines()
        {
            bool [] baseDisciplineArgument = Enumerable.Repeat(true,GVar.DisciplinesNames.Length).ToArray();
            Disciplines baseDisciplines = new Disciplines (baseDisciplineArgument);
            string testData = string.Empty;
            foreach (string discipline in GVar.DisciplinesNames)
                testData += discipline;
            Disciplines testDisciplines =  ContentExtraction.ExtractDisciplines(testData);
            Assert.IsTrue(testDisciplines.ToString() == baseDisciplines.ToString(), "ExtractDisciplines did not extract correctly");
        }
        [TestMethod]
        public void PrinterString()
        {
            Trace.WriteLine("id=\"UW_CO_JOBDTL_DW_UW_CO_EMPUNITDIV\">");
            Assert.IsTrue(true);
        }

    }
}
