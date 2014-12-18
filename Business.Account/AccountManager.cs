using Model.Entities;
using Data.IO.Local;

namespace Business.Account
{
    public class AccountManager
    {
        public UserAccount Account{ get; set; }

        public AccountManager()
        {
            Account = JseLocalRepo.GetAccount();
        }
    }
}
