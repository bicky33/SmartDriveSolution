using Domain.Entities.Payment;

namespace Domain.Repositories.Payment
{
    public interface IRepositoryEntityUserAccount
    {
        Task<IEnumerable<UserAccount>> GetAllEntity(bool trackChanges);

        Task<UserAccount> GetEntityById(int id, bool trackChanges);
        Task<UserAccount> GetUserAccountByAccountNo(string id, bool trackChanges);
        Task<IEnumerable<UserAccount>> GetAllUserAccountByUserId(int id, bool trackChanges);

        void CreateEntity(UserAccount entity);

        void DeleteEntity(UserAccount entity);
    }
}
