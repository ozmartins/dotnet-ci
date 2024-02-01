using FluentValidation;
using Domain.Models.Users;

namespace Domain.Interfaces.Validations
{
    public interface IUserValidation : IValidator<User>
    {
    }
}
