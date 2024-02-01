using Domain.Misc;
using Domain.Models.Users;

namespace Domain.Interfaces.Services
{
    public interface IUserService : IService<User>
    {
        public ServiceResult<User> Create(User user);

        public ServiceResult<User> Update(Guid id, User user);

        public User Get(string emailAddress, string password);

        public ServiceResult<User> UpgradeToSupplier(Guid id, User user);
    }
}
