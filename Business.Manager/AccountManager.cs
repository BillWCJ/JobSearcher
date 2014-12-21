using Data.IO.Local;
using Model.Entities;

namespace Business.Manager
{
    public class AccountManager
    {
        public UserAccount Account{ get; set; }

        public AccountManager()
        {
            Account = new JseLocalRepo().GetAccount();
        }
    }
}
