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
        AHSUnspecified = 4,
        [Description("AHS-Health Promotion")]
        AHSHealthPromotion = 110,
        [Description("AHS-Hlth Studies & Gerontology")]
        AHSHealthStudiesAndGerontology = 5,
        [Description("AHS-Kinesiology")]
        AHSKinesiology = 6,
        [Description("AHS-Rec. & Leisure Studies")]
        AHSRecLeisureStudies = 7,
        [Description("AHS-Rec./Business Management")]
        AHSRecBusinessManagement = 8,
        [Description("ARCH-Architecture")]
        ARCHArchitecture = 31,
        [Description("ARTS MASTERS-Economics")]
        ARTSMASTERSEconomics = 92,
        [Description("ARTS MASTERS-Exp Digital Media")]
        ARTSMASTERSExpDigitalMedia = 90,
        [Description("ARTS MASTERS-Literary Studies")]
        ARTSMASTERSLiteraryStudies = 88,
        [Description("ARTS MASTERS-Political Science")]
        ARTSMASTERSPoliticalScience = 102,
        [Description("ARTS MASTERS-Public Service")]
        ARTSMASTERSPublicService = 91,
        [Description("ARTS MASTERS-Rhet/Comm Design")]
        ARTSMASTERSRhetCommDesign = 89,
        [Description("ARTS-(unspecified)")]
        ARTSUnspecified = 9,
        [Description("ARTS-Anthropology")]
        ARTSAnthropology = 10,
        [Description("ARTS-Arts & Business")]
        ARTSArtsAndBusiness = 68,
        [Description("ARTS-Digital Arts Comm")]
        ARTSDigitalArtsComm = 77,
        [Description("ARTS-Economics")]
        ARTSEconomics = 11,
        [Description("ARTS-English")]
        ARTSEnglish = 13,
        [Description("ARTS-English Lit & Rhetoric")]
        ARTSEnglishLitAndRhetoric = 107,
        [Description("ARTS-Financial Management")]
        ARTSFinancialManagement = 76,
        [Description("ARTS-GlobalDef Engagement")]
        ARTSGlobalEngagement = 108,
        [Description("ARTS-HR Management")]
        ARTSHRManagement = 15,
        [Description("ARTS-History")]
        ARTSHistory = 14,
        [Description("ARTS-International Trade")]
        ARTSInternationalTrade = 106,
        [Description("ARTS-Legal Studies")]
        ARTSLegalStudies = 112,
        [Description("ARTS-Management Accounting")]
        ARTSManagementAccounting = 80,
        [Description("ARTS-Mathematical Economics")]
        ARTSMathematicalEconomics = 111,
        [Description("ARTS-Philosophy")]
        ARTSPhilosophy = 78,
        [Description("ARTS-Political Science")]
        ARTSPoliticalScience = 16,
        [Description("ARTS-Psychology")]
        ARTSPsychology = 17,
        [Description("ARTS-Rhetoric & Prof Writing")]
        ARTSRhetoricAndProfWriting = 69,
        [Description("ARTS-Sociology")]
        ARTSSociology = 18,
        [Description("ARTS-Speech Communication")]
        ARTSSpeechCommunication = 113,
        [Description("All Business (unspecified)")]
        AllBusinessUnSpecified = 65,
        [Description("All Finance (unspecified)")]
        AllFinanceUnSpecified = 109,
        [Description("All Health Informatics")]
        AllHealthInformatics = 81,
        [Description("All Info Tech (unspecified)")]
        AllInfoTechUnSpecified = 66,
        [Description("CA-Chart Prof Acct (CPA)")]
        CAChartProfAcctCPA = 2,
        [Description("ENG MASTERS-Civil")]
        ENGMASTERSCivil = 94,
        [Description("ENG MASTERS-Management Science")]
        ENGMASTERSManagementScience = 93,
        [Description("ENG-(unspecified)")]
        ENGUnSpecified = 19,
        [Description("ENG-Chemical")]
        ENGChemical = 20,
        [Description("ENG-Civil")]
        ENGCivil = 21,
        [Description("ENG-Computer")]
        ENGComputer = 22,
        [Description("ENG-Electrical")]
        ENGElectrical = 23,
        [Description("ENG-Environmental")]
        ENGEnvironmental = 82,
        [Description("ENG-Geological")]
        ENGGeological = 26,
        [Description("ENG-Management")]
        ENGManagement = 83,
        [Description("ENG-Mechanical")]
        ENGMechanical = 28,
        [Description("ENG-Mechatronics")]
        ENGMechatronics = 70,
        [Description("ENG-Nanotechnology")]
        ENGNanotechnology = 79,
        [Description("ENG-Software")]
        ENGSoftware = 45,
        [Description("ENG-Systems Design")]
        ENGSystemsDesign = 29,
        [Description("ENV- (unspecified)")]
        ENVUnSpecified = 30,
        [Description("ENV-Env & Resource Studies")]
        ENVEnvAndResourceStudies = 32,
        [Description("ENV-Environment & Business")]
        ENVEnvironmentAndBusiness = 71,
        [Description("ENV-Geog & Env Management")]
        ENVGeogAndEnvManagement = 33,
        [Description("ENV-Geomatics")]
        ENVGeomatics = 84,
        [Description("ENV-International Development")]
        ENVInternationalDevelopment = 97,
        [Description("ENV-Knowledge Integratio")]
        ENVKnowledgeIntegration = 96,
        [Description("ENV-Planning")]
        ENVPlanning = 34,
        [Description("MATH MASTERS-Health Info")]
        MATHMASTERSHealthInfo = 95,
        [Description("MATH- (unspecified)")]
        MATHUnspecified = 36,
        [Description("MATH-Actuarial Science")]
        MATHActuarialScience = 35,
        [Description("MATH-Applied Mathematics")]
        MATHAppliedMathematics = 37,
        [Description("MATH-Bioinformatics")]
        MATHBioinformatics = 38,
        [Description("MATH-Business Administration")]
        MATHBusinessAdministration = 39,
        [Description("MATH-Combinatorics & Optimizat")]
        MATHCombinatoricsAndOptimizat = 40,
        [Description("MATH-Computational Math")]
        MATHComputationalMath = 72,
        [Description("MATH-Computer Science")]
        MATHComputerScience = 41,
        [Description("MATH-Computing & Financial Mgm")]
        MATHComputingAndFinancialMgm = 104,
        [Description("MATH-Fin Analysis & Risk Mgmt")]
        MATHFinAnalysisAndRiskMgmt = 101,
        [Description("MATH-IT Management")]
        MATHITManagement = 98,
        [Description("MATH-Mathematical Economics")]
        MATHMathematicalEconomics = 99,
        [Description("MATH-Mathematical Finance")]
        MATHMathematicalFinance = 100,
        [Description("MATH-Mathematical Optimization")]
        MATHMathematicalOptimization = 42,
        [Description("MATH-Mathematical Physics")]
        MATHMathematicalPhysics = 73,
        [Description("MATH-Mathematical Studies")]
        MATHMathematicalStudies = 74,
        [Description("MATH-Pure Mathematics")]
        MATHPureMathematics = 43,
        [Description("MATH-Scientific Computation")]
        MATHScientificComputation = 44,
        [Description("MATH-Statistics")]
        MATHStatistics = 46,
        [Description("MATH-Statistics for Health")]
        MATHStatisticsforHealth = 105,
        [Description("MATH-Teaching")]
        MATHTeaching = 63,
        [Description("SCI- (unspecified)")]
        SCIUnspecified = 47,
        [Description("SCI-Biochemistry")]
        SCIBiochemistry = 48,
        [Description("SCI-Bioinformatics")]
        SCIBioinformatics = 49,
        [Description("SCI-Biology")]
        SCIBiology = 50,
        [Description("SCI-Biotechnology/Economics")]
        SCIBiotechnologyEconomics = 51,
        [Description("SCI-Chemistry")]
        SCIChemistry = 52,
        [Description("SCI-Earth Sciences")]
        SCIEarthSciences = 53,
        [Description("SCI-Environmental Science")]
        SCIEnvironmentalScience = 54,
        [Description("SCI-Geology and Hydrogeology")]
        SCIGeologyandHydrogeology = 56,
        [Description("SCI-Optometry")]
        SCIOptometry = 87,
        [Description("SCI-Pharmacy")]
        SCIPharmacy = 86,
        [Description("SCI-Physics")]
        SCIPhysics = 58,
        [Description("SCI-Psychology")]
        SCIPsychology = 103,
        [Description("SCI-Science/Business")]
        SCIScienceBusiness = 60
    }
    public partial class GlobalDef
    {
        /// <summary>
        /// Maximum number of discipline a single job can have (may change according to JobMine or expansion)
        /// </summary>
        public const int MaxNumberOfDisciplinesPerJob = 5;

        /// <summary>
        /// Total Number of Disciplines (may change according to JobMine)
        /// </summary>
        public int NumberOfDisciplines
        {
            get { return DisciplinesNames.Count; }    
        }

        /// <summary>
        /// Highest JobMine discipline number represented in the DisciplineEnum
        /// </summary>
        public const int HighestDisciplineEnumNumber = 113;

        /// <summary>
        /// List of discipline names using their JobMine discipline number as key. Value is the name show on JobMine Job Inquiry
        /// </summary>
        public static Dictionary<byte, string> DisciplinesNames = new Dictionary<byte, string>
        {
            {4,"AHS-(unspecified)"},
            {110,"AHS-Health Promotion"},
            {5,"AHS-Hlth Studies & Gerontology"},
            {6,"AHS-Kinesiology"},
            {7,"AHS-Rec. & Leisure Studies"},
            {8,"AHS-Rec./Business Management"},
            {31,"ARCH-Architecture"},
            {92,"ARTS MASTERS-Economics"},
            {90,"ARTS MASTERS-Exp Digital Media"},
            {88,"ARTS MASTERS-Literary Studies"},
            {102,"ARTS MASTERS-Political Science"},
            {91,"ARTS MASTERS-Public Service"},
            {89,"ARTS MASTERS-Rhet/Comm Design"},
            {9,"ARTS-(unspecified)"},
            {10,"ARTS-Anthropology"},
            {68,"ARTS-Arts & Business"},
            {77,"ARTS-Digital Arts Comm"},
            {11,"ARTS-Economics"},
            {13,"ARTS-English"},
            {107,"ARTS-English Lit & Rhetoric"},
            {76,"ARTS-Financial Management"},
            {108,"ARTS-GlobalDef Engagement"},
            {15,"ARTS-HR Management"},
            {14,"ARTS-History"},
            {106,"ARTS-International Trade"},
            {112,"ARTS-Legal Studies"},
            {80,"ARTS-Management Accounting"},
            {111,"ARTS-Mathematical Economics"},
            {78,"ARTS-Philosophy"},
            {16,"ARTS-Political Science"},
            {17,"ARTS-Psychology"},
            {69,"ARTS-Rhetoric & Prof Writing"},
            {18,"ARTS-Sociology"},
            {113,"ARTS-Speech Communication"},
            {65,"All Business (unspecified)"},
            {109,"All Finance (unspecified)"},
            {81,"All Health Informatics"},
            {66,"All Info Tech (unspecified)"},
            {2,"CA-Chart Prof Acct (CPA)"},
            {94,"ENG MASTERS-Civil"},
            {93,"ENG MASTERS-Management Science"},
            {19,"ENG-(unspecified)"},
            {20,"ENG-Chemical"},
            {21,"ENG-Civil"},
            {22,"ENG-Computer"},
            {23,"ENG-Electrical"},
            {82,"ENG-Environmental"},
            {26,"ENG-Geological"},
            {83,"ENG-Management"},
            {28,"ENG-Mechanical"},
            {70,"ENG-Mechatronics"},
            {79,"ENG-Nanotechnology"},
            {45,"ENG-Software"},
            {29,"ENG-Systems Design"},
            {30,"ENV- (unspecified)"},
            {32,"ENV-Env & Resource Studies"},
            {71,"ENV-Environment & Business"},
            {33,"ENV-Geog & Env Management"},
            {84,"ENV-Geomatics"},
            {97,"ENV-International Development"},
            {96,"ENV-Knowledge Integration"},
            {34,"ENV-Planning"},
            {95,"MATH MASTERS-Health Info"},
            {36,"MATH- (unspecified)"},
            {35,"MATH-Actuarial Science"},
            {37,"MATH-Applied Mathematics"},
            {38,"MATH-Bioinformatics"},
            {39,"MATH-Business Administration"},
            {40,"MATH-Combinatorics & Optimizat"},
            {72,"MATH-Computational Math"},
            {41,"MATH-Computer Science"},
            {104,"MATH-Computing & Financial Mgm"},
            {101,"MATH-Fin Analysis & Risk Mgmt"},
            {98,"MATH-IT Management"},
            {99,"MATH-Mathematical Economics"},
            {100,"MATH-Mathematical Finance"},
            {42,"MATH-Mathematical Optimization"},
            {73,"MATH-Mathematical Physics"},
            {74,"MATH-Mathematical Studies"},
            {43,"MATH-Pure Mathematics"},
            {44,"MATH-Scientific Computation"},
            {46,"MATH-Statistics"},
            {105,"MATH-Statistics for Health"},
            {63,"MATH-Teaching"},
            {47,"SCI- (unspecified)"},
            {48,"SCI-Biochemistry"},
            {49,"SCI-Bioinformatics"},
            {50,"SCI-Biology"},
            {51,"SCI-Biotechnology/Economics"},
            {52,"SCI-Chemistry"},
            {53,"SCI-Earth Sciences"},
            {54,"SCI-Environmental Science"},
            {56,"SCI-Geology and Hydrogeology"},
            {87,"SCI-Optometry"},
            {86,"SCI-Pharmacy"},
            {58,"SCI-Physics"},
            {103,"SCI-Psychology"},
            {60,"SCI-Science/Business"}
        };
    }
}
