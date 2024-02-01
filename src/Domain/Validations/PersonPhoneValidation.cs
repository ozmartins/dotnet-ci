using FluentValidation;
using Domain.Interfaces.Validations;
using Domain.Models.People;

namespace Domain.Validations
{
    public class PersonPhoneValidation : AbstractValidator<Person>, IPersonPhoneValidation
    {
        public PersonPhoneValidation()
        {
            RuleFor(person => phoneDuplicated(person)).Equal(false).WithMessage("A pessoa informada possui telefones duplicados.");
        }
        private bool phoneDuplicated(Person person)
        {
            var result = person.Phones
                .GroupBy(x => x.Prefix + x.Number)
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);

            return result.Count() > 0;
        }
    }
}
