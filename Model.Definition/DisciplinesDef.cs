using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Definition
{
    /// <summary>
    /// Enum of different discipline name matching their JobMine discipline number
    /// </summary>
    public enum DisciplineEnum : byte
    {
        [Description("Unknown (Please Pick a Discipline)")]
        UnAssigned = 0,
        [Description("AHS-(unspecified)")]
        AhsUnspecified = 4,
        [Description("AHS-Health Promotion")]
        AhsHealthPromotion = 110,
        [Description("AHS-Hlth Studies & Gerontology")]
        AhsHealthStudiesAndGerontology = 5,
        [Description("AHS-Kinesiology")]
        AhsKinesiology = 6,
        [Description("AHS-Rec. & Leisure Studies")]
        AhsRecLeisureStudies = 7,
        [Description("AHS-Rec./Business Management")]
        AhsRecBusinessManagement = 8,
        [Description("ARCH-Architecture")]
        ArchArchitecture = 31,
        [Description("ARTS MASTERS-Economics")]
        ArtsmastersEconomics = 92,
        [Description("ARTS MASTERS-Exp Digital Media")]
        ArtsmastersExpDigitalMedia = 90,
        [Description("ARTS MASTERS-Literary Studies")]
        ArtsmastersLiteraryStudies = 88,
        [Description("ARTS MASTERS-Political Science")]
        ArtsmastersPoliticalScience = 102,
        [Description("ARTS MASTERS-Public Service")]
        ArtsmastersPublicService = 91,
        [Description("ARTS MASTERS-Rhet/Comm Design")]
        ArtsmastersRhetCommDesign = 89,
        [Description("ARTS-(unspecified)")]
        ArtsUnspecified = 9,
        [Description("ARTS-Anthropology")]
        ArtsAnthropology = 10,
        [Description("ARTS-Arts & Business")]
        ArtsArtsAndBusiness = 68,
        [Description("ARTS-Digital Arts Comm")]
        ArtsDigitalArtsComm = 77,
        [Description("ARTS-Economics")]
        ArtsEconomics = 11,
        [Description("ARTS-English")]
        ArtsEnglish = 13,
        [Description("ARTS-English Lit & Rhetoric")]
        ArtsEnglishLitAndRhetoric = 107,
        [Description("ARTS-Financial Management")]
        ArtsFinancialManagement = 76,
        [Description("ARTS-GlobalDef Engagement")]
        ArtsGlobalEngagement = 108,
        [Description("ARTS-HR Management")]
        ArtshrManagement = 15,
        [Description("ARTS-History")]
        ArtsHistory = 14,
        [Description("ARTS-International Trade")]
        ArtsInternationalTrade = 106,
        [Description("ARTS-Legal Studies")]
        ArtsLegalStudies = 112,
        [Description("ARTS-Management Accounting")]
        ArtsManagementAccounting = 80,
        [Description("ARTS-Mathematical Economics")]
        ArtsMathematicalEconomics = 111,
        [Description("ARTS-Philosophy")]
        ArtsPhilosophy = 78,
        [Description("ARTS-Political Science")]
        ArtsPoliticalScience = 16,
        [Description("ARTS-Psychology")]
        ArtsPsychology = 17,
        [Description("ARTS-Rhetoric & Prof Writing")]
        ArtsRhetoricAndProfWriting = 69,
        [Description("ARTS-Sociology")]
        ArtsSociology = 18,
        [Description("ARTS-Speech Communication")]
        ArtsSpeechCommunication = 113,
        [Description("All Business (unspecified)")]
        AllBusinessUnSpecified = 65,
        [Description("All Finance (unspecified)")]
        AllFinanceUnSpecified = 109,
        [Description("All Health Informatics")]
        AllHealthInformatics = 81,
        [Description("All Info Tech (unspecified)")]
        AllInfoTechUnSpecified = 66,
        [Description("All Chart Prof Acct (CPA)")]
        CaChartProfAcctCpa = 2,
        [Description("ENG MASTERS-Civil")]
        EngmastersCivil = 94,
        [Description("ENG MASTERS-Management Science")]
        EngmastersManagementScience = 93,
        [Description("ENG-(unspecified)")]
        EngUnSpecified = 19,
        [Description("ENG-Chemical")]
        EngChemical = 20,
        [Description("ENG-Civil")]
        EngCivil = 21,
        [Description("ENG-Computer")]
        EngComputer = 22,
        [Description("ENG-Electrical")]
        EngElectrical = 23,
        [Description("ENG-Environmental")]
        EngEnvironmental = 82,
        [Description("ENG-Geological")]
        EngGeological = 26,
        [Description("ENG-Management")]
        EngManagement = 83,
        [Description("ENG-Mechanical")]
        EngMechanical = 28,
        [Description("ENG-Mechatronics")]
        EngMechatronics = 70,
        [Description("ENG-Nanotechnology")]
        EngNanotechnology = 79,
        [Description("ENG-Software")]
        EngSoftware = 45,
        [Description("ENG-Systems Design")]
        EngSystemsDesign = 29,
        [Description("ENV- (unspecified)")]
        EnvUnSpecified = 30,
        [Description("ENV-Env & Resource Studies")]
        EnvEnvAndResourceStudies = 32,
        [Description("ENV-Environment & Business")]
        EnvEnvironmentAndBusiness = 71,
        [Description("ENV-Geog & Env Management")]
        EnvGeogAndEnvManagement = 33,
        [Description("ENV-Geomatics")]
        EnvGeomatics = 84,
        [Description("ENV-International Development")]
        EnvInternationalDevelopment = 97,
        [Description("ENV-Knowledge Integratio")]
        EnvKnowledgeIntegration = 96,
        [Description("ENV-Planning")]
        EnvPlanning = 34,
        [Description("MATH MASTERS-Health Info")]
        MathmastersHealthInfo = 95,
        [Description("MATH- (unspecified)")]
        MathUnspecified = 36,
        [Description("MATH-Actuarial Science")]
        MathActuarialScience = 35,
        [Description("MATH-Applied Mathematics")]
        MathAppliedMathematics = 37,
        [Description("MATH-Bioinformatics")]
        MathBioinformatics = 38,
        [Description("MATH-Business Administration")]
        MathBusinessAdministration = 39,
        [Description("MATH-Combinatorics & Optimizat")]
        MathCombinatoricsAndOptimizat = 40,
        [Description("MATH-Computational Math")]
        MathComputationalMath = 72,
        [Description("MATH-Computer Science")]
        MathComputerScience = 41,
        [Description("MATH-Computing & Financial Mgm")]
        MathComputingAndFinancialMgm = 104,
        [Description("MATH-Fin Analysis & Risk Mgmt")]
        MathFinAnalysisAndRiskMgmt = 101,
        [Description("MATH-IT Management")]
        MathitManagement = 98,
        [Description("MATH-Mathematical Economics")]
        MathMathematicalEconomics = 99,
        [Description("MATH-Mathematical Finance")]
        MathMathematicalFinance = 100,
        [Description("MATH-Mathematical Optimization")]
        MathMathematicalOptimization = 42,
        [Description("MATH-Mathematical Physics")]
        MathMathematicalPhysics = 73,
        [Description("MATH-Mathematical Studies")]
        MathMathematicalStudies = 74,
        [Description("MATH-Pure Mathematics")]
        MathPureMathematics = 43,
        [Description("MATH-Scientific Computation")]
        MathScientificComputation = 44,
        [Description("MATH-Statistics")]
        MathStatistics = 46,
        [Description("MATH-Statistics for Health")]
        MathStatisticsforHealth = 105,
        [Description("MATH-Teaching")]
        MathTeaching = 63,
        [Description("SCI- (unspecified)")]
        SciUnspecified = 47,
        [Description("SCI-Biochemistry")]
        SciBiochemistry = 48,
        [Description("SCI-Bioinformatics")]
        SciBioinformatics = 49,
        [Description("SCI-Biology")]
        SciBiology = 50,
        [Description("SCI-Biotechnology/Economics")]
        SciBiotechnologyEconomics = 51,
        [Description("SCI-Chemistry")]
        SciChemistry = 52,
        [Description("SCI-Earth Sciences")]
        SciEarthSciences = 53,
        [Description("SCI-Environmental Science")]
        SciEnvironmentalScience = 54,
        [Description("SCI-Geology and Hydrogeology")]
        SciGeologyandHydrogeology = 56,
        [Description("SCI-Optometry")]
        SciOptometry = 87,
        [Description("SCI-Pharmacy")]
        SciPharmacy = 86,
        [Description("SCI-Physics")]
        SciPhysics = 58,
        [Description("SCI-Psychology")]
        SciPsychology = 103,
        [Description("SCI-Science/Business")]
        SciScienceBusiness = 60
    }
    public partial class GlobalDef
    {
        /// <summary>
        /// Maximum number of discipline a single job can have (may change according to JobMine or expansion)
        /// </summary>
        public const int MaxNumberOfDisciplinesPerJob = 5;

        /// <summary>
        /// Highest JobMine discipline number represented in the DisciplineEnum
        /// </summary>
        public const int HighestDisciplineEnumNumber = 113;
    }
}
