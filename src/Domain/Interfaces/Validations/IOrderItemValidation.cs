using FluentValidation;
using Domain.Models.Orders;

namespace Domain.Interfaces.Validations
{
    public interface IOrderItemValidation : IValidator<OrderItem>
    {
    }
}
