using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class UserAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string GoogleApisServerKey { get; set; }
        public string GoogleApisBrowserKey { get; set; }
        public string FilePath { get; set; }

        public UserAccount()
        {
            FilePath = GetFilePath();
        }

        private string GetFilePath()
        {
            string cwd = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string solutionPath = cwd.Replace("\\bin", "").Replace("\\Debug", "").Replace("\\obj", "").Replace("\\Release", "").TrimEnd('\\') + '\\';
            return solutionPath;
        }
    }
}
