using FluentValidation;
using Domain.Interfaces.Validations;
using Domain.Models.Notications;

namespace Domain.Validations
{
    public class NotificationValidation : AbstractValidator<Notification>, INotificationValidation
    {
        public NotificationValidation()
        {
            RuleFor(p => p.Destination).NotNull().WithMessage("O destinatário da notificação não foi informado.");

            RuleFor(p => p.Text).NotEmpty().WithMessage("O texto da notificação não foi informado.");

            RuleFor(p => p.DateTime).GreaterThan(DateTime.MinValue).WithMessage("A data da notificação não foi informada.");
        }
    }
}
