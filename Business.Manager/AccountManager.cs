using Data.IO.Local;
using Model.Entities;

namespace Business.Manager
{
    public class AccountManager
    {
        IJseLocalRepo JseLocalRepo { get; set; }
        public AccountManager(IJseLocalRepo jseLocalRepo)
        {
            JseLocalRepo = jseLocalRepo;
        }

        public UserAccount GetUserAccount()
        {
            return JseLocalRepo.GetAccount();
        }
    }
}
