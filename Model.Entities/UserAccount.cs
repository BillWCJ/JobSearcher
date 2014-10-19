using System;
using System.Collections.Generic;
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
        public string MasterFilePath { get; set; }
        public string FilePath { get; set; }

        public UserAccount()
        {
            MasterFilePath = @"C:\Users\BillWenChao\Dropbox\Software Projects\";
            FilePath = MasterFilePath + @"JobSearchEnhancer\";
        }
    }
}
