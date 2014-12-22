using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Data.Contract.Local;
using Model.Definition;
using Model.Entities;

namespace Data.IO.Local
{
    public class JseLocalRepo : IJseLocalRepo
    {
        private const string DataFileName = @"ProgramData.txt";
        public JseLocalRepo()
        {
            FilePath = GetSolutionPath() ?? CommonDef.DefaultFilePath;
        }

        public static string FilePath { get; private set; }

        public UserAccount GetAccount()
        {
            UserAccount account = null;
            string dataFilePath = FilePath + DataFileName;
            StreamReader reader = StreamReader.Null;
            try
            {
                reader = new StreamReader(dataFilePath);
                String username = reader.ReadLine();
                String password = reader.ReadLine();
                String googleApisServerKey = reader.ReadLine();
                String googleApisBrowserKey = reader.ReadLine();
                if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    throw new ArgumentException();
                account = new UserAccount
                {
                    FilePath = FilePath,
                    JobMineUsername = username,
                    JobMinePassword = password,
                    GoogleApisServerKey = googleApisServerKey,
                    GoogleApisBrowserKey = googleApisBrowserKey
                };
            }
            catch (Exception e)
            {
                Trace.Write(e.ToString());
                Console.WriteLine("No Data File Found or Data File Empty");
                CreateDataFile();
                return GetAccount();
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return account;
        }

        private static string GetSolutionPath()
        {
            try
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);

                string directoryName = Path.GetDirectoryName(path);
                if (directoryName == null) throw new NullReferenceException("directoryName is null");
                string projectPath = directoryName.Replace("obj", "").Replace("bin", "").Replace("Debug", "").Replace("Release", "").TrimEnd('\\');

                string solutionPath = Path.GetDirectoryName(projectPath);
                if (solutionPath == null) throw new NullReferenceException("solutionPath is null");
                return solutionPath.TrimEnd('\\') + '\\';
            }
            catch (Exception e)
            {
                Trace.Write(e.ToString());
                return CommonDef.DefaultFilePath;
            }
        }

        private static void CreateDataFile()
        {
            string userName = null;
            string passWord = null;
            try
            {
                Console.WriteLine("Please enter UserName");
                userName = Console.ReadLine().TrimEnd('\n');
                Console.WriteLine("Please enter JobMinePassword");
                passWord = Console.ReadLine().TrimEnd('\n');
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var writer = StreamWriter.Null;
            var account = new UserAccount();
            string userInfoFilePath = FilePath + DataFileName;
            try
            {
                writer = new StreamWriter(userInfoFilePath);
                writer.WriteLine(userName);
                writer.WriteLine(passWord);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            if (writer != null)
                writer.Close();
        }
    }
}