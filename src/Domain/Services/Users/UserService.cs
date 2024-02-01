using Domain.Misc;
using Domain.Interfaces;
using Domain.Interfaces.Filters;
using Domain.Interfaces.Services;
using Domain.Interfaces.Validations;
using Domain.Models.Users;

namespace Domain.Services.Users
{
    public class UserService : IUserService
    {
        private IUserValidation _userValidation;

        private IFilterBuilder<User> _filterBuilder;

        private IRepository<User> _repository;

        private BasicService<User> _basicService;

        public UserService(IRepository<User> repository, IUserValidation userValidation, IFilterBuilder<User> filterBuilder)
        {
            _userValidation = userValidation;
            _filterBuilder = filterBuilder;
            _repository = repository;
            _basicService = new BasicService<User>(repository, userValidation);
        }

        public ServiceResult<User> Create(User user)
        {
            user.ChangeUserRole(UserRole.Customer);

            user.ChangeUserPassword(user.Password);

            return _basicService.Create(user);
        }

        public ServiceResult<User> Update(Guid id, User user)
        {
            user.ChangeUserPassword(user.Password);

            return _basicService.Update(id, user);
        }

        public ServiceResult<User> Delete(Guid id)
        {
            return _basicService.Delete(id);
        }

        public User Get(Guid id)
        {
            return _basicService.Get(id);
        }

        public List<User> Get()
        {
            return _basicService.Get();
        }

        public User Get(string emailAddress, string password)
        {
            var hash = User.GeneratePasswordHash(emailAddress, password);

            _filterBuilder
                .Equal(x => x.EmailAddress, emailAddress)
                .Equal(x => x.Password, hash);

            return _repository.Recover(_filterBuilder).FirstOrDefault() ?? new User();
        }

        public ServiceResult<User> UpgradeToSupplier(Guid id, User user)
        {
            var currentUser = Get(user.Id);

            if (currentUser == null)
                return ServiceResult<User>.FailureResult("Não foi possível localizar o usuário informado.");

            currentUser.ChangeUserRole(UserRole.Supplier);

            var result = _userValidation.Validate(user);

            if (!result.IsValid)
                return ServiceResult<User>.FailureResult(result);

            _repository.Update(id, user);

            return ServiceResult<User>.SuccessResult(user);
        }
    }
}
