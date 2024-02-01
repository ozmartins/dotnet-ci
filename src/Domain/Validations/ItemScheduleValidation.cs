using FluentValidation;
using Domain.Interfaces.Validations;
using Domain.Models.Items;

namespace Domain.Validations
{
    public class ItemScheduleValidation : AbstractValidator<Item>, IItemScheduleValidation
    {
        public ItemScheduleValidation()
        {
            RuleFor(p => scheduleDuplicated(p)).Equal(false).WithMessage("O item possui mais de uma agenda aberta para o mesmo dia da semana.");

            RuleFor(p => scheduleItemInConflict(p)).Equal(false).WithMessage("O item possui um ou mais dias da semana com horários conflitantes.");
        }

        private bool scheduleDuplicated(Item item)
        {
            var result = item.Schedules
                .GroupBy(x => x.DayOfWeek)
                .Where(g => g.Count() > 1)
                .Select(x => x.Key);

            return result.Count() > 0;
        }

        private bool scheduleItemInConflict(Item item)
        {
            foreach (var schedule in item.Schedules)
            {
                foreach (var currentScheduleItem in schedule.Items)
                {
                    var equalHours = schedule
                        .Items
                        .Where(otherScheduleItem =>
                            otherScheduleItem.Id != currentScheduleItem.Id &&
                            otherScheduleItem.InitialHour == currentScheduleItem.InitialHour &&
                            otherScheduleItem.FinalHour == currentScheduleItem.FinalHour);

                    var hoursWhereInitialHourItIsInsideTheRange = schedule
                        .Items
                        .Where(otherScheduleItem =>
                            otherScheduleItem.Id != currentScheduleItem.Id &&
                            otherScheduleItem.InitialHour < currentScheduleItem.InitialHour &&
                            currentScheduleItem.InitialHour < otherScheduleItem.FinalHour);

                    var hoursWhereFinalHourItIsInsideTheRange = schedule
                        .Items
                        .Where(otherScheduleItem =>
                            otherScheduleItem.Id != currentScheduleItem.Id &&
                            otherScheduleItem.InitialHour < currentScheduleItem.FinalHour &&
                            currentScheduleItem.FinalHour < otherScheduleItem.FinalHour);

                    if (equalHours.Count() > 0 || hoursWhereInitialHourItIsInsideTheRange.Count() > 0 || hoursWhereFinalHourItIsInsideTheRange.Count() > 0)
                        return true;
                }
            }

            return false;
        }
    }
}
