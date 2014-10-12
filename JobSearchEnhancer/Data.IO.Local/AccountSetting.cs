using System;
using System.IO;
using Model.Entities;

namespace Data.IO.Local
{
    public class AccountSetting
    {
        public static UserAccount GetAccount()
        {
            var account = new Model.Entities.UserAccount();
            string userInfoFilePath = account.MasterFilePath + @"JobMineLogIn.txt";
            var reader = StreamReader.Null;
            try
            {
                reader = new StreamReader(userInfoFilePath);
                String username = reader.ReadLine();
                String password = reader.ReadLine();
                String googleApisServerKey = reader.ReadLine();
                String googleApisBrowserKey = reader.ReadLine();
                account.Username = username;
                account.Password = password;
                account.GoogleApisServerKey = googleApisServerKey;
                account.GoogleApisBrowserKey = googleApisBrowserKey;
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
            return account;
        }
    }
}