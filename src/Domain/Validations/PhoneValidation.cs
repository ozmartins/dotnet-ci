using FluentValidation;
using Domain.Interfaces.Validations;
using Domain.Models.People;

namespace Domain.Validations
{
    public class PhoneValidation : AbstractValidator<Phone>, IPhoneValidation
    {
        public PhoneValidation()
        {
            RuleFor(x => x.Prefix).NotEmpty().WithMessage("O prefixo do número do telefone não foi informado.");

            RuleFor(x => x.Prefix).Length(2).WithMessage("O prefixo do número do telefone deve conter exatamentee dois dígitos.");

            RuleFor(x => x.Number).NotEmpty().WithMessage("O número do telefone não foi informado.");

            RuleFor(x => x.Number).Length(8, 11).WithMessage("O número do telefone deve conter entre oito e onze dígitos.");

            RuleFor(x => x.Prefix).Must(x => x.All(char.IsDigit)).WithMessage("O prefixo do telefone deve conter apenas números.");

            RuleFor(x => x.Number).Must(x => x.All(char.IsDigit)).WithMessage("O número do telefone deve conter apenas números.");
        }
    }
}