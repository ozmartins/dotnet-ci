using FluentValidation;
using Domain.Models.Cities;

namespace Domain.Interfaces.Validations
{
    public interface ICityValidation : IValidator<City>
    {
    }
}
