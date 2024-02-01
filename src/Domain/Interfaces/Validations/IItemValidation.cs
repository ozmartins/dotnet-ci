using FluentValidation;
using FluentValidation.Results;
using Domain.Models.Items;

namespace Domain.Interfaces.Validations
{
    public interface IItemValidation : IValidator<Item>
    {
        public ValidationResult CustomValidate(Item item);
    }
}
