using System;
using System.Diagnostics;
using System.IO;
using Model.Entities;

namespace Data.IO.Local
{
    public class AccountSetting
    {
        private const string DataFileName = @"ProgramData.txt";

        public static UserAccount GetAccount()
        {
            var account = new UserAccount();
            string userInfoFilePath = account.FilePath + DataFileName;
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
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                CreateDataFile();
                return GetAccount();
            }
            if (reader != null)
            {
                reader.Close();
            }
            return account;
        }

        private static void CreateDataFile()
        {
            string userName = null;
            string passWord = null;
            try
            {
                Console.WriteLine("Please enter UserName");
                userName = Console.ReadLine().TrimEnd('\n');
                Console.WriteLine("Please enter Password");
                passWord = Console.ReadLine().TrimEnd('\n');
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var writer = StreamWriter.Null;
            var account = new UserAccount();
            string userInfoFilePath = account.FilePath + DataFileName;
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
            if(writer != null)
                writer.Close();
        }
    }
}