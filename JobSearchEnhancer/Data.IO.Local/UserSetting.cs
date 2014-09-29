using System;
using System.IO;
using GlobalVariable;

namespace Data.IO.Local
{
    public class UserSetting
    {
        public static void GetAccount()
        {
            var reader = StreamReader.Null;
            try
            {
                reader = new StreamReader(GVar.UserInfoFile);
                String username = reader.ReadLine();
                String password = reader.ReadLine();
                String googleApisServerKey = reader.ReadLine();
                String googleApisBrowserKey = reader.ReadLine();
                GVar.Account = new UserAccount
                {
                    Username = username,
                    Password = password,
                    GoogleApisServerKey = googleApisServerKey,
                    GoogleApisBrowserKey = googleApisBrowserKey
                };
                reader.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("!Error-{0}_In_{1}: {2}\n", e.GetType(), "Gvar", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("!Error-{0}_In_{1}: {2}\n", e.GetType(), "Gvar", e.Message);
            }
            if (reader != null)
            {
                reader.Close();
            }
        }
    }
}