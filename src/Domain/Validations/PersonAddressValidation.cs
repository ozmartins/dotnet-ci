using FluentValidation;
using Domain.Interfaces.Validations;
using Domain.Models.People;

namespace Domain.Validations
{
    public class PersonAddressValidation : AbstractValidator<Person>, IPersonAddressValidation
    {
        public PersonAddressValidation()
        {
            RuleFor(person => addressDuplicated(person)).Equal(false).WithMessage("A pessoa informada possui endereços duplicados.");
        }
        private bool addressDuplicated(Person person)
        {
            var result = person.Addresses
                .GroupBy(x => x.ZipCode + x.Number.ToString())
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);

            return result.Count() > 0;
        }
    }
}
