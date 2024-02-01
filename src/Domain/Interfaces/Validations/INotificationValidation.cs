using FluentValidation;
using Domain.Models.Notications;

namespace Domain.Interfaces.Validations
{
    public interface INotificationValidation : IValidator<Notification>
    {
    }
}
