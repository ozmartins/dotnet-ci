using FluentValidation;
using Domain.Models.Messages;

namespace Domain.Interfaces.Validations
{
    public interface IMessageValidation : IValidator<Message>
    {
    }
}
