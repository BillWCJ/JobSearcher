using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Model.Definition;
using Model.Entities;

namespace Data.IO.Local
{
    public class JseLocalRepo : IJseLocalRepo
    {
        public JseLocalRepo()
        {
            FilePath = GetSolutionPath() ?? CommonDef.DefaultFilePath;
        }

        public static string FilePath { get; private set; }

        public static UserAccount GetAccount()
        {
            UserAccount account = null;
            string dataFilePath = FilePath + @"SystemData.txt";
            StreamReader reader = StreamReader.Null;
            try
            {
                reader = new StreamReader(dataFilePath);
                String username = reader.ReadLine();
                String password = reader.ReadLine();
                String googleApisServerKey = reader.ReadLine();
                String googleApisBrowserKey = reader.ReadLine();
                account = new UserAccount
                {
                    FilePath = FilePath,
                    Username = username,
                    Password = password,
                    GoogleApisServerKey = googleApisServerKey,
                    GoogleApisBrowserKey = googleApisBrowserKey
                };
            }
            catch (Exception e)
            {
                Trace.Write(e.ToString());
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
    }
}