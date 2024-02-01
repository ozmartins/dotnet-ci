using FluentValidation;
using FluentValidation.Results;
using Domain.Models.People;

namespace Domain.Interfaces.Validations
{
    public interface IPersonValidation : IValidator<Person>
    {
        public ValidationResult CustomValidate(Person person);
    }
}
