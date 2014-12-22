using Model.Entities;

namespace Data.Contract.Local
{
    public interface IJseLocalRepo
    {
        UserAccount GetAccount();
    }
}