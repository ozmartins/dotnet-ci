using FluentValidation;
using Domain.Models.Items;

namespace Domain.Interfaces.Validations
{
    public interface IScheduleValidation : IValidator<Schedule>
    {
    }
}
