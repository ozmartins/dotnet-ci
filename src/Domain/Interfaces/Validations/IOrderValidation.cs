using FluentValidation;
using FluentValidation.Results;
using Domain.Models.Orders;

namespace Domain.Interfaces.Validations
{
    public interface IOrderValidation : IValidator<Order>
    {
        public ValidationResult CustomValidate(Order order);
    }
}
