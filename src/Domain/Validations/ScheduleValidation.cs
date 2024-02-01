using FluentValidation;
using Domain.Interfaces.Validations;
using Domain.Models.Items;

namespace Domain.Validations
{
    public class ScheduleValidation : AbstractValidator<Schedule>, IScheduleValidation
    {
        public ScheduleValidation()
        {
            RuleFor(x => x.DayOfWeek).IsInEnum().WithMessage("O dia da semana informado é inválido.");

            RuleFor(x => x.Items).NotEmpty().WithMessage("Nenhum horário foi informado.");

            RuleForEach(x => x.Items).Must(hour => hour.InitialHour < hour.FinalHour).WithMessage("A hora inicial precisa ser menor que a hora final.");

            RuleForEach(x => x.Items).Must(hour => hour.InitialHour >= 0 && hour.InitialHour <= 24).WithMessage("A hora inicial precisa estar entre 0 e 24.");

            RuleForEach(x => x.Items).Must(hour => hour.FinalHour >= 0 && hour.FinalHour <= 24).WithMessage("A hora final precisa estar entre 0 e 24.");
        }
    }
}
