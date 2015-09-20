using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Data.Contract.Local;
using Data.EF.JseDb;
using Model.Entities;

namespace Business.Manager
{
    public class UserAccountManager
    {
        public UserAccount UserAccount { get; set; }
        public UserAccountManager()
        {
            try
            {
                using (var db = new JseDbContext())
                {
                    UserAccount = db.UserAccount.FirstOrDefault();
                    if (UserAccount == null)
                    {
                        UserAccount = new UserAccount();
                        db.UserAccount.AddOrUpdate(UserAccount);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}