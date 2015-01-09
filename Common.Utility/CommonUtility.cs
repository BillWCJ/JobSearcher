using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Common.Utility
{
    public static class CommonUtility
    {
        public static string GetSolutionPath()
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
                return null;
            }
        }
    }
}
