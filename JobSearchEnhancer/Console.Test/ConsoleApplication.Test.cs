using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using ConsoleApplication;
using Jobs;

namespace ConsoleApplication.Test {
    [TestClass]
    public class ConsoleApplicationTest {
        [TestMethod]
        public void ExtractDisciplines_ShouldContainALLDisciplines_WhenPassedAllDisciplines() {
            string[] disciplinesNames = {"AHS-(unspecified)","AHS-Health Promotion","AHS-Hlth Studies & Gerontology"
                            ,"AHS-Kinesiology","AHS-Rec. & Leisure Studies",@"AHS-Rec./Business Management"
                            ,"ARCH-Architecture","ARTS MASTERS-Economics","ARTS MASTERS-Exp Digital Media"
                            ,"ARTS MASTERS-Literary Studies","ARTS MASTERS-Political Science","ARTS MASTERS-Public Service"
                            ,@"ARTS MASTERS-Rhet/Comm Design","ARTS-(unspecified)","ARTS-Anthropology"
                            ,"ARTS-Arts & Business","ARTS-Digital Arts Comm","ARTS-Economics"
                            ,"ARTS-English","ARTS-English Lit & Rhetoric","ARTS-Financial Management"
                            ,"ARTS-Global Engagement","ARTS-HR Management","ARTS-History"
                            ,"ARTS-International Trade","ARTS-Legal Studies","ARTS-Management Accounting"
                            ,"ARTS-Mathematical Economics","ARTS-Philosophy","ARTS-Political Science"
                            ,"ARTS-Psychology","ARTS-Rhetoric & Prof Writing","ARTS-Sociology"
                            ,"ARTS-Speech Communication","All Business (unspecified)","All Finance (unspecified)"
                            ,"All Health Informatics","All Info Tech (unspecified)","CA-Chart Prof Acct (CPA)"
                            ,"ENG MASTERS-Civil","ENG MASTERS-Management Science","ENG-(unspecified)"
                            ,"ENG-Chemical","ENG-Civil","ENG-Computer"
                            ,"ENG-Electrical","ENG-Environmental","ENG-Geological"
                            ,"ENG-Management","ENG-Mechanical","ENG-Mechatronics"
                            ,"ENG-Nanotechnology","ENG-Software","ENG-Systems Design"
                            ,"ENV- (unspecified)","ENV-Env & Resource Studies","ENV-Environment & Business"
                            ,"ENV-Geog & Env Management","ENV-Geomatics","ENV-International Development"
                            ,"ENV-Knowledge Integration","ENV-Planning","MATH MASTERS-Health Info"
                            ,"MATH- (unspecified)","MATH-Actuarial Science","MATH-Applied Mathematics"
                            ,"MATH-Bioinformatics","MATH-Business Administration","MATH-Combinatorics & Optimizat"
                            ,"MATH-Computational Math","MATH-Computer Science","MATH-Computing & Financial Mgm"
                            ,"MATH-Fin Analysis & Risk Mgmt","MATH-IT Management","MATH-Mathematical Economics"
                            ,"MATH-Mathematical Finance","MATH-Mathematical Optimization","MATH-Mathematical Physics"
                            ,"MATH-Mathematical Studies","MATH-Pure Mathematics","MATH-Scientific Computation"
                            ,"MATH-Statistics","MATH-Statistics for Health","MATH-Teaching"
                            ,"SCI- (unspecified)","SCI-Biochemistry","SCI-Bioinformatics"
                            ,"SCI-Biology",@"SCI-Biotechnology/Economics","SCI-Chemistry"
                            ,"SCI-Earth Sciences","SCI-Environmental Science","SCI-Geology and Hydrogeology"
                            ,"SCI-Optometry","SCI-Pharmacy","SCI-Physics"
                            ,"SCI-Psychology",@"SCI-Science/Business"
                            };
            string data = string.Empty;
            foreach (string discipline in disciplinesNames)
                data += discipline;
            //unfinished test

        }
    }
}
