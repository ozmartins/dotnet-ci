using FluentValidation;
using Domain.Interfaces;
using Domain.Interfaces.Filters;
using Domain.Interfaces.Validations;
using Domain.Models.Users;
using System.Text.RegularExpressions;

namespace Domain.Validations
{
    public class UserValidation : AbstractValidator<User>, IUserValidation
    {
        public UserValidation(IRepository<User> userRepository, IFilterBuilder<User> filterBuilder)
        {
            RuleFor(p => p.EmailAddress).NotEmpty().WithMessage("Endereço de e-mail não foi informado.");

            RuleFor(p => isEmailValid(p.EmailAddress)).Equal(true).WithMessage("Endereço de e-mail informado é inválido.");

            RuleFor(p => p.Password).NotEmpty().WithMessage("Senha não foi informada.");

            RuleFor(p => doesPasswordRespectPolicy(p.Password)).Equal(true).WithMessage("Senha informada não respeita a política de senhas.");

            RuleFor(p => p.Role).IsInEnum().WithMessage("Papel do usuário é inválido.");

            RuleFor(p => userAlreadyExists(userRepository, filterBuilder, p)).Equal(false).WithMessage("Já existe um usuário cadastrado com o endereço de e-mail informado.");
        }

        private static bool userAlreadyExists(IRepository<User> userRepository, IFilterBuilder<User> filterBuilder, User user)
        {
            filterBuilder
                .Equal(x => x.EmailAddress, user.EmailAddress)
                .Unequal(x => x.Id, user.Id);

            return userRepository.Recover(filterBuilder).Count > 0;
        }

        private static bool doesPasswordRespectPolicy(string password)
        {
            return !string.IsNullOrEmpty(password);
        }

        private static bool isEmailValid(string emailAddress)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(5));
            return regex.IsMatch(emailAddress);
        }
    }
}
