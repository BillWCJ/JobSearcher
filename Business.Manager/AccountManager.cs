using Data.Contract.Local;
using Model.Entities;

namespace Business.Manager
{
    public class AccountManager
    {
        public AccountManager(IJseLocalRepo jseLocalRepo)
        {
            JseLocalRepo = jseLocalRepo;
        }

        private IJseLocalRepo JseLocalRepo { get; set; }

        public UserAccount GetUserAccount()
        {
            return JseLocalRepo.GetAccount();
        }
    }
}