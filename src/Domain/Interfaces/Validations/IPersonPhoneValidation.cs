using FluentValidation;
using Domain.Models.People;

namespace Domain.Interfaces.Validations
{
    public interface IPersonPhoneValidation : IValidator<Person>
    {
    }
}
