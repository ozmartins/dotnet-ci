using FluentValidation;
using Domain.Models.Addresses;

namespace Domain.Interfaces.Validations
{
    public interface IAddressValidation : IValidator<Address>
    {
    }
}
